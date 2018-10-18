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
}