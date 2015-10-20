using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NinjaDomain.Classes
{
    public class Ninja
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Elite { get; set; }
        public Clan clan { get; set; }
        public int ClanID { get; set; }
        public List<NinjaEquipment> Equipment { get; set; } 
    }

    public class Clan
    {
        public int Id { get; set; }
        public string ClanName { get; set; }
    }

    public class NinjaEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        [Required]
        public Ninja Ninja { get; set; }
    }

    public enum EquipmentType
    {
        SmokeBomb,Katana,Shruiken,FalseBeard,CardboardBox
    }
}
