using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dummy.Controllers
{
    public class HomeController : Controller
    {

        internal class Orario{
            public DateTime Quando { get; set; }
            public string Stato { get; set; }
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("time")]
        public IActionResult Time()
        {
            string nextStatus = "alla fine";
            DateTime mo = DateTime.Now;
            TimeSpan quantoManca;
            if( mo.Hour >= 18 && mo.Hour < 9 ){
                DateTime leNove = DateTime.Parse("09:00").AddDays(1);
                quantoManca = leNove.Subtract(mo);
                nextStatus = "all'inizio";
            }else if( mo.Hour >= 9 && mo.Hour < 11 ){
                DateTime leUndici = DateTime.Parse("11:00");
                quantoManca = leUndici.Subtract(mo);
                nextStatus = "alla pausa del mattino";
            }else if( mo.Hour >= 11 && mo.Hour < 13 ){
                DateTime leTredici = DateTime.Parse("13:00");
                quantoManca = leTredici.Subtract(mo);
                nextStatus = "alla pausa pranzo";
            }else if(mo.Hour >= 13 && mo.Hour < 14 ){
                DateTime leQuattordici = DateTime.Parse("14:00");
                quantoManca = leQuattordici.Subtract(mo);
                nextStatus = "alla fine della pausa pranzo";
            }else if(mo.Hour >= 14 && mo.Hour < 16 ){
                DateTime leSedici = DateTime.Parse("16:00");
                quantoManca = leSedici.Subtract(mo);
                nextStatus = "alla pausa pomeridiana";
            }else if(mo.Hour >= 16 && mo.Hour < 18 ){
                DateTime leDiciotto = DateTime.Parse("18:00");
                quantoManca = leDiciotto.Subtract(mo);
                nextStatus = "alla fine della giornata";
            }
            int hours = quantoManca.Hours;
            int minutes = quantoManca.Minutes;
            int seconds = quantoManca.Seconds;
            string hoursDescription = hours == 1 ? "ora" : "ore";
            string minutesDescription = minutes == 1 ? "minuto" : "minuti";
            string secondsDescription = seconds == 1 ? "secondo" : "secondi";
            string risultato = String.Format( "Mancano {0} {1}, {2} {3}, {4} {5} {6}!",
                hours, hoursDescription,
                minutes, minutesDescription,
                seconds, secondsDescription,
                nextStatus );
            return Json( new { result = risultato });
        }

        [ActionName("times")]
        public IActionResult Times()
        {
            string nextStatus = "alla fine";
            DateTime mo = DateTime.Now;
            Orario[] orari = new Orario[]{
                new Orario(){
                    Quando = DateTime.Parse("09:00"),
                    Stato = "all'inizio della giornata"
                },
                new Orario(){
                    Quando = DateTime.Parse("11:00"),
                    Stato = "alla pausa del mattino"
                },
                new Orario(){
                    Quando = DateTime.Parse("13:00"),
                    Stato = "alla pausa pranzo"
                },
                new Orario(){
                    Quando = DateTime.Parse("14:00"),
                    Stato = "alla fine della pausa pranzo"
                },
                new Orario(){
                    Quando = DateTime.Parse("16:00"),
                    Stato = "alla pausa pomeridiana"
                },
                new Orario(){
                    Quando = DateTime.Parse("18:00"),
                    Stato = "alla fine della giornata"
                }
            };
            List<string> risultati = new List<string>();
            foreach( Orario orario in orari ){
                string risultatoParziale = "";
                if( mo.CompareTo(orario.Quando ) > 0 ){
                    risultatoParziale = String.Format("Mancano 0 secondi {0}!", orario.Stato);
                }else{
                    TimeSpan quantoManca = orario.Quando.Subtract(mo);
                    int hours = quantoManca.Hours;
                    int minutes = quantoManca.Minutes;
                    int seconds = quantoManca.Seconds;
                    string hoursDescription = hours == 1 ? "ora" : "ore";
                    string minutesDescription = minutes == 1 ? "minuto" : "minuti";
                    string secondsDescription = seconds == 1 ? "secondo" : "secondi";
                    risultatoParziale = String.Format( "Mancano {0} {1}, {2} {3}, {4} {5} {6}!",
                        hours, hoursDescription,
                        minutes, minutesDescription,
                        seconds, secondsDescription,
                        nextStatus );
                }
                risultati.Add(risultatoParziale);
            }
            
            return Json( new { results = risultati });
        }

        [HttpGet]
        [ActionName("content")]
        public IActionResult GetFileContent()
        {
            string content = System.IO.File.ReadAllText("/Users/Simone/Projects/EdilportaleNotes/Module D/07032017.md");
            return Json( new { content = content } );
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
