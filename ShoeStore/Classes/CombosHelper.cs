namespace ShoeStore.Classes
{

    using ShoeStore.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class CombosHelper : IDisposable
    {


        private static DataContext db = new DataContext();

        //public static List<State> GetStates()
        //{
        //    var Estados = db.Estadoes.ToList();
        //    Estados.Add(new Estado
        //    {
        //        IdEstado = 0,
        //        Descripcion = "[--Selecciona un Estado--]",
        //    });

        //    return Estados.OrderBy(e => e.Descripcion).ToList();
        //}

        //public static List<Municipio> GetMunicipios()
        //{
        //    var Municipio = db.Municipios.ToList();
        //    Municipio.Add(new Municipio
        //    {
        //        IdMunicipio = 0,
        //        Descripcion = "[--Selecciona un Municipio--]",
        //    });

        //    return Municipio.OrderBy(e => e.Descripcion).ToList();
        //}
        //public static List<Municipio> GetMunicipios(int idEstado)
        //{
        //    var Municipio = db.Municipios.Where(m => m.IdEstado == idEstado).ToList();
        //    Municipio.Add(new Municipio
        //    {
        //        IdMunicipio = 0,
        //        Descripcion = "[--Selecciona un Municipio--]",
        //    });

        //    return Municipio.OrderBy(e => e.Descripcion).ToList();
        //}


        //public static List<Colonia> GetColonias()
        //{
        //    var Colonia = db.Colonias.ToList();
        //    Colonia.Add(new Colonia
        //    {
        //        IdColonia = 0,
        //        Descripcion = "[--Selecciona una Colonia--]",
        //    });

        //    return Colonia.OrderBy(e => e.Descripcion).ToList();
        //}

        //public static List<Colonia> GetColonias(int idMunicipio)
        //{
        //    var Colonia = db.Colonias.Where(m => m.IdMunicipio == idMunicipio).ToList();
        //    Colonia.Add(new Colonia
        //    {
        //        IdColonia = 0,
        //        Descripcion = "[--Selecciona una Colonia--]",
        //    });

        //    return Colonia.OrderBy(e => e.Descripcion).ToList();
        //}

        public void Dispose()
        {
            db.Dispose();
        }
    }
}