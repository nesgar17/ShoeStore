namespace ShoeStore.Classes
{
    using ShoeStore.Models;
    using System;

    public class DbHelper
    {

        public static Response SaveChanges(DataContext db)
        {
            try
            {
                db.SaveChanges();
                return new Response { Successfully = true, };
            }
            catch (Exception ex)
            {
                var response = new Response { Successfully = false, };
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("_Index"))
                {
                    response.Message = "Hay registros con el mismo valor";
                }
                else if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    response.Message = "El registro no puede ser eliminado, contiene información relacionada";
                }
                else
                {
                    response.Message = ex.Message;
                }

                return response;
            }

        }


        public static void InsertBitacora(string accion, string table, string user, DataContext db)
        {

            var bitacora = new Bitacora
            {
                Accion = accion,
                Table = table,
                User = user,
                DateofInsert = DateTime.Now,

            };

            db.Bitacoras.Add(bitacora);
            db.SaveChanges();

        }

    }
}