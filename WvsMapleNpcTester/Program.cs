using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

namespace WvsMapleNpcTester
{
    class Program
    {
        static void Main(string[] args)
        {
            NpcTester tester = new NpcTester();

            Player me = new Player("Tagette", 100, 412)
            {
                Strength = 5,
                Intelligence = 5,
                Dexterity = 32767,
                Luck = 32767
            };

            me.Inventory.GainItem(4031013, 30);

            tester.AddNpc(2100, new begin_jp1());
            tester.AddNpc(9101001, new begin_jp2());
            tester.AddNpc(2101, new begin_jp3());
            tester.AddNpc(2003, new begin5());
            tester.AddNpc(1043000, new bush1());
            tester.AddNpc(1043001, new bush2());
            tester.AddNpc(1072002, new change_archer());
            tester.AddNpc(1072006, new inside_archer());
            tester.AddNpc(1061010, new job3Exit());

            tester.Run();
        }
    }
}
