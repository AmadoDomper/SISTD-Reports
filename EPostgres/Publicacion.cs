using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPostgres
{
    public class Publicacion
    {
        public int pub_idpublicacion { get; set; }
        public int pub_anopublicacion { get; set; }
        public DateTime pub_fechaRegistro { get; set; }
        public string pub_referenciabibliografica { get; set; }
        public DateTime pub_fechaaceptado { get; set; }
        public string pub_enlace { get; set; }
        public Tipo oTipo { get; set; }
        public Tema oTema { get; set; }
    }
}
