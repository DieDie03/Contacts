using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable

namespace Contacts.DAL.Entities
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Tel { get; set; }
        public string Email { get; set; }

    }

}
