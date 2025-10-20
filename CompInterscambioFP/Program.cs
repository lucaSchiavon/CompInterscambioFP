using CompInterscambioFP.Managers;
using CompInterscambioFP.Models;
using CompInterscambioFP.Models.CSV;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CompInterscambioFP
{
    class Program
    {
        static void Main(string[] args)
        {
 
            var manager = new SettingsManager(Path.Combine(Environment.CurrentDirectory, "") + @"\Settings.ini");
            Settings settings = manager.LoadSettings();

            try
            {

                //controlli esistenze cartelle
                if (string.IsNullOrEmpty(settings.MeccanoplasticaPercorso) || !Directory.Exists(settings.MeccanoplasticaPercorso))
                {
                    LogManager.ScriviLog($"Percorso non esistente: {settings.MeccanoplasticaPercorso}");
                    return;
                }
                if (string.IsNullOrEmpty(settings.MeccanoplasticaOld) || !Directory.Exists(settings.MeccanoplasticaOld))
                {
                    LogManager.ScriviLog($"Cartella OLD non esistente: {settings.MeccanoplasticaPercorso}");
                    return;
                }
                if (string.IsNullOrEmpty(settings.MeccanoplasticaErrori) || !Directory.Exists(settings.MeccanoplasticaErrori))
                {
                    LogManager.ScriviLog($"Cartella Errori non esistente: {settings.MeccanoplasticaErrori}");
                    return;
                }


             
                if (Debugger.IsAttached)
                    Console.WriteLine($"Elaborazione cartella: {settings.MeccanoplasticaPercorso}");


                    var filesCsv = Directory.GetFiles(settings.MeccanoplasticaPercorso, "*.csv");

                    var SqlTransactionManager = new SqlTransactionManager(settings.ConnStr);

                    foreach (var file in filesCsv)
                    {
                        try
                        {
                        if (Debugger.IsAttached)
                            Console.WriteLine($"Lettura file: {file}");

                            var righe = File.ReadAllLines(file);
                            if (righe.Length < 2)
                                throw new Exception($"Il file CSV {file} non contiene dati validi.");

                            // Parsing della seconda riga (dati)
                            //var valori = righe[1].Split(',').Select(s => s.Trim('"')).ToArray();
                        var valori = righe[1].Split(';');
                        var dto = new DatiMacchinaDto
                            {
                                DataOra = valori[0],
                                Ricetta = valori[1],
                                PezziBuoni = int.Parse(valori[2]),
                                PezziScarto = int.Parse(valori[3]),                            
                            };

                        SqlTransactionManager.MeccanoplasticaEffettuaCaricoScarico(() =>
                        {
                            //Delegate: eseguito subito prima del commit
                            //uso il pattern inversion o control per far si che lo spostamento
                            //del file di output sia atomico
                            string destFile = Path.Combine(settings.MeccanoplasticaOld, Path.GetFileName(file));
                            Directory.CreateDirectory(settings.MeccanoplasticaOld);
                            File.Move(file, destFile);

                            if (Debugger.IsAttached)
                                Console.WriteLine($"File spostato in OLD: {destFile}");
                        });

                       

                        }
                        catch (Exception exFile)
                        {
                            LogManager.ScriviLog($"Errore elaborando {file}: {exFile.Message}\n{exFile.StackTrace}");

                            try
                            {
                                Directory.CreateDirectory(settings.MeccanoplasticaErrori);
                                string errorFile = Path.Combine(settings.MeccanoplasticaErrori, Path.GetFileName(file));
                                File.Move(file, errorFile);
                            }
                            catch { /* se fallisce anche qui, ignoriamo per evitare loop */ }

                            
                        }
                    }
              
            }
            catch (Exception ex)
            {
                LogManager.ScriviLog($"Errore generale: {ex.Message}\n{ex.StackTrace}");
            }

            if (Debugger.IsAttached)
                Console.WriteLine("Elaborazione completata.");
           

        }
    }
}
