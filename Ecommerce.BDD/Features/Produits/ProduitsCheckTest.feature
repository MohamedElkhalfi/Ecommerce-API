Feature: Affichage de tous les produits.
    En tant qu'utilisateur
    Je veux vérifier si l'application affiche tous les produits

 
 @Scenario_initial
Scenario: Affichage de tous les produits
    Given qu'il existe des produits dans la base de données
    When je demande à l'application d'afficher les produits
    Then l'application affiche tous les produits
