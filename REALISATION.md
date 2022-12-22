# Analyse de projet
- Pour créer une dépense, il est neccessaire de créer les apis qui sont en lien avec l'objet dépense 
- Pour vérifier l'indentique de la devise de la dépense et celle de l'ulitisateur, elle est donc référencé de tous deux éléments

# Conception
Les objects principaux de l'application web: la Dépense, l'utilisateur, la devise sont en relation OneToMany
- Une dépense appartient à un et un seul utilisateur(1,1). Un utilisateur ne peut rien avoir ou avoir plusieurs dépenses(0,n)
- Une dépense est enregitré avec une et une seule devise(1,1). Une devise ne peut attribuer à aucune dépense ou à plusieurs dépenses(0,n)
- Un utilisateur associe avec une et une seule devise(1,1). Une devise peut ne pas appartenir à personne ou à plusieurs utilisateurs(0,n).

# Choix de technologies
- Creation de projet avec ASP.NET CORE 5.0 avec API WEB
- Tous les packages MicrosoftEntityFramworkCore version 5.0.17
- Entity Framework Code First 
- Les principes SOLID peuvent être apperçu dans la manière de la création des interfaces séparées
- Pour la persistance de donnée, j'ai utilisé le modèle DTO(Data Access Objects) comme une couche proche de la 
base de données, il est plus fréquent de changer la façon d'effectuer la persistance que de changer le modèle DAO (Data Access Object) et elle peut être 
utilisé pour transporter les données entre les différente couche distantes
- Utiliser ASP.NET Core structure MVC : Model, View, Controller et ViewModel

# Test API
Pour tester les API en lien avec la base de données local
- Swagger
- Postman

# Test xUnit
- Tester les regles de validations pour la création de dépense 