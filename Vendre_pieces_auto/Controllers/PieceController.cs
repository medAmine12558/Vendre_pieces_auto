using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Vendre_pieces_auto.Data;
using Vendre_pieces_auto.Models.Tabels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Vendre_pieces_auto.Models.Views;
using static System.Net.Mime.MediaTypeNames;

namespace Vendre_pieces_auto.Controllers
{
    public class PieceController : Controller
    {
        private readonly Context _context; // Ajoutez une variable privée pour stocker le contexte

        public PieceController(Context context)
        {
            _context = context; // Injectez le contexte dans le constructeur
        }

        public IActionResult Page_Ajouter()
        {
            if (User.Identity.IsAuthenticated)
            { //si user authentifie je vais le rederiger vers la page d'ajout de piece

                return View("Views/Piece/Ajouter_Piece.cshtml");

            }
            else// si non je vais le rederiger vers la page d'autentifier 
            {
                return RedirectToAction("Login_Piece", "Autho");//ici on a appeler le controleur "AuthoController" avec la methode "Login_Piece" qui va lui meme appeler le processus d'auth
            }
        }
        public IActionResult Detaille()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Detaille_pagePiece_check", "Autho");
            }
            else
            {
                return View();

            }
        }

        [HttpPost]
        public async Task<IActionResult> Ajouter_piece(List<IFormFile> image, Piece piece)
        {
            
            piece.image = string.Empty;//initialiser la colone image dans piece par empty
            if (image == null || image.Count == 0)//verifier est ce que les images qui sont passes comme parametre dans la variable image dans cette methode est elle vide ou non(ca veut dire est ce que le user a choisis des images ou non
            {
                Console.WriteLine("pas de photo");
            }
            else
            {
                foreach (var file in image)//un iterateur dans la variable image qui est passe comme parametre de cette methode qui contient une liste des images choisits
                {
                    
                    if(string.IsNullOrEmpty(piece.image))//verifier est ce que on n'est dans la premiere itteration (pour eviter la "," comme le premier caractere du variable)
                    {
                        var fileName=Path.GetFileName(file.FileName);//recuperer le non du photo
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;//ajouter une valeur alliatoire et unique dans le non du photo pour eviter la redendence ou niveau des noms des photos
                        var filePath = Path.Combine("wwwroot/Images", uniqueFileName);//la copie dans le path wwwroot/Images
                        using(var stream = System.IO.File.Create(filePath))//cree un fichier avec les parametres qui sont definies dans la variable filePath et utiliser la variable stream pour pointer sur cette tache
                        {
                            await file.CopyToAsync(stream);//executer le creation du nouveau ficher et librer les ressource systeme
                        }
                        
                        piece.image = filePath;//ajouter le nouveau url du photo cree (url) dans les etapes precedante au colone image dans la table Piece
                    }
                    else//efectuer le meme tretement precedent sur les autre iterations qui sont pas la premiere
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                        var filePath = Path.Combine("wwwroot/Images", uniqueFileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                       
                        piece.image += "," + filePath;//separer entre les urls des image par une vergule
                    }
                }
            }
            
            piece.Id_Vendeur = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;//ajouter le id de user courant dans la colone id_vendeur dans la table Piece
            _context.Piece.Add(piece);//ajouter la ligne a la table Piece
            _context.SaveChanges();//executer le stockage de ligne dans la table Piece
            return View("~/Views/Piece/Page_Confiramtion.cshtml");

            /*piece.image = string.Empty;
            if (productImages == null || productImages.Count == 0)
             {
                 Console.WriteLine("pas de photo");
                 return RedirectToAction("Ajouter", "Piece");
             }
             else
             {
                 foreach (var file in productImages)
                 {
                    // Afficher le nom du fichier dans la console du serveur
                    if (string.IsNullOrEmpty(piece.image))
                    {
                        piece.image = file.FileName;
                    }
                    else
                    {
                        piece.image += "," + file.FileName;
                    }
                }
                 

                 // Rediriger vers une autre vue/action si nécessaire
                
             }
            ModelState.SetModelValue("image", new ValueProviderResult(piece.image, CultureInfo.CurrentCulture));

            Console.WriteLine(piece.image);
            //piece.Id_Vendeur = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();

            if (ModelState.IsValid)
            {
               
                _context.Piece.Add(piece);
                await _context.SaveChangesAsync();
                return RedirectToAction("~/Views/Piece/Page_Confiramtion.cstml");
            }
            else
            {
                var erreurs = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                foreach (var erreur in erreurs)
                {
                    Console.WriteLine($"Champ: {erreur.Key}, Erreur: {string.Join(", ", erreur.Errors.Select(e => e.ErrorMessage))}");
                }

                return RedirectToAction("~/Views/Piece/Page_Confiramtion.cstml");
            }*/
        }
    }
    }

