using System;
using System.Data.SqlClient;

namespace CompInterscambioFP.Managers
{
    public class SqlTransactionManager : IDisposable
    {

        private readonly SqlConnection SqlConn;
        //private SqlTransaction transaction;
        public void MeccanoplasticaEffettuaCaricoScarico(Action onBeforeCommit = null)
        {
            SqlTransaction transaction = SqlConn.BeginTransaction();

            try
            {


                string Ins = "INSERT INTO [dbo].[TblDatiMacchinaDto] ([DataOra],[NomeRicetta],[PezziBuoni],[PezziScartati],[PezziCartone],[CartoniBuoni]) " +
                    "VALUES ('2025-10-20T14:35:00', N'RICETTA_ABC_123', 1500, 25, 50, 30)";
                
                ExecuteNonQuery(SqlConn, transaction, Ins);

                
                string Ins2 = "INSERT INTO [dbo].[TblDatiMacchinaDto] ([DataOra],[NomeRicetta],[PezziBuoni],[PezziScartati],[PezziCartone],[CartoniBuoni]) " +
                    "VALUES ('2025-10-21T14:37:00', N'RICETTA_CDE_456', 1600, 35, 55, 40)";
                ExecuteNonQuery(SqlConn, transaction, Ins2);

                //qui invoco il delegate per spostare in old il file
                onBeforeCommit?.Invoke();

                // se arrivo qui --> commit
                transaction.Commit();

            }
            catch (Exception ex)
            {
                // rollback in caso di errore
                transaction.Rollback();

                // rilancio l'eccezione per gestirla a livello superiore
                throw new Exception("Rollback inserimento carico scarico: " + ex.Message, ex);
            }

        }

        public SqlTransactionManager(string ConStr)
        {
            SqlConn = new SqlConnection(ConStr);
            SqlConn.Open();
        }

        
        private static void ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, string sqlQuery)
        {
            using (SqlCommand command = new SqlCommand(sqlQuery, connection, transaction))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            if (SqlConn.State == System.Data.ConnectionState.Open)
                SqlConn.Close();
        }

    }
}
