using Ecommerce.Core.Model;
using Ecommerce.DataAccess.ConnexionDB;
using Ecommerce.DataAccess.Model;
using System;
using TechTalk.SpecFlow;

namespace Ecommerce.BDD.StepDefinitions
{
    [Binding]
    public class AffichageDeTousLesProduits_StepDefinitions
    {
        private EcommerceContext _context { get; }
        private List<Product> _products ;
        public AffichageDeTousLesProduits_StepDefinitions(EcommerceContext Context )
        {
            _context = Context; 
        }


        [Given(@"qu'il existe des produits dans la base de données")]
        public void GivenQuilExisteDesProduitsDansLaBaseDeDonnees()
        {
            ProduitsAttendus();

            _context.Product.AddRange(ProduitsAttendus());
            _context.SaveChanges();
        }

      

        [When(@"je demande à l'application d'afficher les produits")]
        public void WhenJeDemandeALApplicationDAfficherLesProduits()
        {
            // Code pour appeler la fonctionnalité d'affichage des produits
            // Assurez-vous que l'application affiche les produits correctement

            var displayedProducts = GetProductsDisplayedByApplication();

            // Vérifiez si les produits sont correctement affichés
            // Utilisez des assertions pour valider les résultats
            Assert.NotNull(displayedProducts);
            Assert.True(displayedProducts.Count > 0);
        }


        [Then(@"l'application affiche tous les produits")]
        public void ThenLapplicationAfficheTousLesProduits()
        {
            // Vérifiez que l'application affiche tous les produits
            var produitsAffiches = GetProductsDisplayedByApplication();

            Assert.Equal(3, produitsAffiches.Count);

            foreach (var produitAttendu in ProduitsAttendus())
            {
                Assert.Contains(produitAttendu, produitsAffiches);
            }
        }

        private List<Product> GetProductsDisplayedByApplication()
        {
            return _context.Product.ToList();
        }
        private List<Product> ProduitsAttendus()
        {
            return _products = new List<Product>
            {
                new Product { Name = "Produit 1", CurrentPrice = 10.0M },
                new Product { Name = "Produit 2", CurrentPrice = 20.0M },
                new Product { Name = "Produit 3", CurrentPrice = 30.0M }
            };
        }
    }
}
