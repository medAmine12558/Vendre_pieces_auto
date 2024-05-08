﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Vendre_pieces_auto.Controllers
{
    public class User_InterfaceController : Controller
    {
        public IActionResult InterfaceUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;//recuperer le uid d'utilsateur connecter
                Console.WriteLine(userId);
            }
            return View();
        }
        public IActionResult Ajouter_Piece()//cette methode verifie lorsque le user veux ajouter une piece est ce qu'il est authentifie ou non
        {
            if(User.Identity.IsAuthenticated)//si user authentifie je vais le rederiger vers la page d'ajout de piece
            return View();
            else// si non je vais le rederiger vers la page d'autentifier 
            {
                return RedirectToAction("Login_Piece", "Autho");//ici on a appeler le controleur "AuthoController" avec la methode "Login_Piece" qui va lui meme appeler le processus d'auth
            }
        }
    }
    
}
