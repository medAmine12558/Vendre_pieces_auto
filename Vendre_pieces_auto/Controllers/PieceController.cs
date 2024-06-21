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
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public IActionResult Detaille(int id)
        {

           
            if (!User.Identity.IsAuthenticated)
            {
               

                return RedirectToAction("Detaille_pagePiece_check", "Autho" , new {id=id});
            }
            else
            {
                if(id != null){
                    var pieceWithPhotos = _context.Piece
                        .Include(p => p.Photos)
                        .SingleOrDefault(p => p.Id_piece == id);//faire une jointure pour recuperer les photos ayont le id de photo
                    
                    if (pieceWithPhotos != null)
                    {
                        foreach (var p in pieceWithPhotos.Photos)
                        {
                           
                            Console.WriteLine(p.image);//afficher les urls des image
                        }
                    }
                    var sug = _context.Piece.Include(p => p.Photos).Where(p => p.Type_name.Equals(pieceWithPhotos.Type_name));
                    return View(new { pieceWithPhotos = pieceWithPhotos , sug=sug});
                }


                return View();



            }
        }

        [HttpPost]
        public async Task<IActionResult> Ajouter_piece(List<IFormFile> image, Piece piece)
        {
            var uniqueFileName = "";

            //piece.image = string.Empty;//initialiser la colone image dans piece par empty
            if (image == null || image.Count == 0)//verifier est ce que les images qui sont passes comme parametre dans la variable image dans cette methode est elle vide ou non(ca veut dire est ce que le user a choisis des images ou non
            {
                Console.WriteLine("pas de photo");
            }
            else
            {
                foreach (var file in image)//un iterateur dans la variable image qui est passe comme parametre de cette methode qui contient une liste des images choisits
                {
                    
                   
                        var fileName=Path.GetFileName(file.FileName);//recuperer le non du photo
                         uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;//ajouter une valeur alliatoire et unique dans le non du photo pour eviter la redendence ou niveau des noms des photos
                        var filePath = Path.Combine("wwwroot/Images/", uniqueFileName);//la copie dans le path wwwroot/Images
                    filePath = filePath.Replace(" ", "-");
                    using(var stream = System.IO.File.Create(filePath))//cree un fichier avec les parametres qui sont definies dans la variable filePath et utiliser la variable stream pour pointer sur cette tache
                        {
                            await file.CopyToAsync(stream);//executer le creation du nouveau ficher et librer les ressource systeme
                        }
                        
                    var p = new Photos
                    {
                        image = filePath,//affecter l'url d'image dans le champs correspendent dans la table photos
                        Piece = piece//affecter id du piece dans le champs correspendent dans la table photos
                    };
                    _context.Photos.Add(p);//ajouter le neuveau objet photo dans la table photos
                }
            }
            Console.WriteLine(uniqueFileName);
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

        public IActionResult Ajouter_favoris(int id)
        {
            Favoris f=new Favoris
            {
                Id_client=HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                piece=_context.Piece.FirstOrDefault(x=>x.Id_piece==id)
            };
            _context.Favoris.Add(f);
            _context.SaveChanges() ;
            return Json( new { success = true});
        }
        public IActionResult Afficher_favoris()
        {
            string client = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var favoris_piece = _context.Favoris.Include(x=>x.piece).ThenInclude(p => p.Photos).Where(x => x.Id_client.Equals(client));
            
            
            
            return View(favoris_piece);
        }
        public IActionResult Supp_favoris(int id)
        {
            Favoris f = _context.Favoris.FirstOrDefault(x => x.Id== id);
            if (f != null)
            {
                _context.Favoris.Remove(f);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
            
        }

    }
    }

