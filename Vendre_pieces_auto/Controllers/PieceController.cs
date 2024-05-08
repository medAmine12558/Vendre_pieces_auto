using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Vendre_pieces_auto.Controllers
{
    public class PieceController : Controller
    {
        public IActionResult Ajouter()
        {
            if (User.Identity.IsAuthenticated) { //si user authentifie je vais le rederiger vers la page d'ajout de piece
                
                return View("Views/Piece/Ajouter_Piece.cshtml");

             }
            else// si non je vais le rederiger vers la page d'autentifier 
            {
                return RedirectToAction("Login_Piece", "Autho");//ici on a appeler le controleur "AuthoController" avec la methode "Login_Piece" qui va lui meme appeler le processus d'auth
            }
        }
        public IActionResult Detaille()
        {
            if(!User.Identity.IsAuthenticated) {
                return RedirectToAction("Detaille_pagePiece_check", "Autho"); 
            }
            else
            {
                return View();

            }
        }
    }
}
