# Analyse de projet
- Pour cr�er une d�pense, il est neccessaire de cr�er les apis qui sont en lien avec l'objet d�pense 
- Pour v�rifier l'indentique de la devise de la d�pense et celle de l'ulitisateur, elle est donc r�f�renc� de tous deux �l�ments

# Conception
Les objects principaux de l'application web: la D�pense, l'utilisateur, la devise sont en relation OneToMany
- Une d�pense appartient � un et un seul utilisateur(1,1). Un utilisateur ne peut rien avoir ou avoir plusieurs d�penses(0,n)
- Une d�pense est enregitr� avec une et une seule devise(1,1). Une devise ne peut attribuer � aucune d�pense ou � plusieurs d�penses(0,n)
- Un utilisateur associe avec une et une seule devise(1,1). Une devise peut ne pas appartenir � personne ou � plusieurs utilisateurs(0,n).

# Choix de technologies
- Creation de projet avec ASP.NET CORE 5.0 avec API WEB
- Tous les packages MicrosoftEntityFramworkCore version 5.0.17
- Entity Framework Code First 
- Les principes SOLID peuvent �tre apper�u dans la mani�re de la cr�ation des interfaces s�par�es
- Pour la persistance de donn�e, j'ai utilis� le mod�le DTO(Data Access Objects) comme une couche proche de la 
base de donn�es, il est plus fr�quent de changer la fa�on d'effectuer la persistance que de changer le mod�le DAO (Data Access Object) et elle peut �tre 
utilis� pour transporter les donn�es entre les diff�rente couche distantes
- Utiliser ASP.NET Core structure MVC : Model, View, Controller et ViewModel

# Test API
Pour tester les API en lien avec la base de donn�es local
- Swagger
- Postman

# Test xUnit
- Tester les regles de validations pour la cr�ation de d�pense 