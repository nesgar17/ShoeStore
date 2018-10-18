namespace ShoeStore.Classes
{

    using ShoeStore.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class CombosHelper : IDisposable
    {


        private static DataContext db = new DataContext();

        public static List<State> GetStates()
        {
            var states = db.States.ToList();
            states.Add(new State
            {
                IdState = 0,
                Description = "[--Selecciona un Estado--]",
            });

            return states.OrderBy(e => e.Description).ToList();
        }

        public static List<Municipality> GetMunicipalities()
        {
            var municipalities = db.Municipalities.ToList();
            municipalities.Add(new Municipality
            {
                IdMunicipality = 0,
                Description = "[--Selecciona un Municipio--]",
            });

            return municipalities.OrderBy(e => e.Description).ToList();
        }
        public static List<Municipality> GetMunicipalities(int idState)
        {
            var municipalities = db.Municipalities.Where(m => m.IdState == idState).ToList();
            municipalities.Add(new Municipality
            {
                IdMunicipality = 0,
                Description = "[--Selecciona un Municipio--]",
            });

            return municipalities.OrderBy(e => e.Description).ToList();
        }


        public static List<Colony> GetColonies()
        {
            var colonies = db.Colonies.ToList();
            colonies.Add(new Colony
            {
                IdColony = 0,
                Description = "[--Selecciona una Colonia--]",
            });

            return colonies.OrderBy(e => e.Description).ToList();
        }

        public static List<Colony> GetColonies(int idMunicipality)
        {
            var colonies = db.Colonies.Where(m => m.IdMunicipality == idMunicipality).ToList();
            colonies.Add(new Colony
            {
                IdColony = 0,
                Description = "[--Selecciona una Colonia--]",
            });

            return colonies.OrderBy(e => e.Description).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}