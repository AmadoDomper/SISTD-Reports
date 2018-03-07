using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESql;

namespace VisorPub.Models
{
    public class RegistrarAvanceViewModel
    {
        public List<Combo> lsPOIsVigentes;
        public List<Combo> lsMotivoRetraso;
        public List<Combo> lsMetas;
        public List<Combo> lsPeriodoCale;
        public int idUser;
    }
}