# TPServer

**Pré-requis**
Avoir dotnet 6
(Rider ou Visual Studio)
Avoir SQLServer d'installé 


1. Cloner le dépot
2. Lancer le projet sur rider ou VS
3. Utiliser le terminal, se mettre dans le chemin du projet et lancer la commande suivante : 

**dotnet ef database update**

4.  Lancer le projet et utiliser le Swagger.




----------------------------------- Documentation ---------------------------------------

## **Channel** 

**GET** */api/Channel* 

Récupérer la lise de tous les channels

**POST** */api/Channel*

Créer un Channel

**GET** */api/Channel/{id}*

Récupérer un channel avec son id

**PUT** */api/Channel/{id}*

Modifier un Channel

**DELETE** */api/Channel/{id}*

Supprimer un Channel 

**GET** */api/Channel/{id}/Messages*

Récupérer la liste des Messages d'un channel


## **Message** 

**GET** */api/Message* 

Récupérer la lise de tous les Messages

**POST** */api/Message*

Créer un Message

**GET** */api/Message/{id}*

Récupérer un Message avec son id

**PUT** */api/Message/{id}*

Modifier un Message

**DELETE** */api/Message/{id}*

Supprimer un Message 


## **Server** 

**GET** */api/Server* 

Récupérer la lise de tous les Server

**POST** */api/Server*

Créer un Server

**GET** */api/Server/{id}*

Récupérer un Server avec son id

**PUT** */api/Server/{id}*

Modifier un Server

**DELETE** */api/Server/{id}*

Supprimer un Server 

**POST** /api/Server/{idServer}/Join/{idUser}

Rejoindre un Server

**POST** /api/Server/{idServer}/Admin/{idUser}

RAjouter un Admin sur un Server



## **USER** 

**GET** */api/USER* 

Récupérer la lise de tous les USER

**POST** */api/USER*

Créer un USER

**GET** */api/USER/{id}*

Récupérer un USER avec son id

**PUT** */api/USER/{id}*

Modifier un USER

**DELETE** */api/USER/{id}*

Supprimer un USER


(Bug avec le swagger HUB du coup j'ai mit la doc ici aussi)
Lien du swaggerHUB : https://app.swaggerhub.com/apis/LadYnov/server-message_api/1.0#/Channel/get_api_Channel 


------------------------------------------ ## SÉCURITÉ ------------------------------------------

## Injection / chiffrement des données :
il faut traiter (Canonicalization, Escaping & Sanitization) et faire valider par l'API (et aussi côté client) les données reçues pour eviter de se retrouver avec des données malicieuses (exemple : SQL injection) qui risquerait de manipuler la base de données

N'autoriser que les inputs ayant des caractères alphabétiques et numériques (grâce à une regex), ce qui évitera d'avoir des caractères spéciaux

<br><br>

## Cookies :
il faut éviter d'utiliser les cookies qui risqueraient de nous exposer aux attaques CSRF

<br><br>

## Authentification / Autorisation :
Mettre en place un système d'authentification pour éviter des abus d'utilisation, par exemple : des vols de données ou un trop grand nombre de requêtes effectué ce qui risquerait de faire un DDOS

L'utilisateur devra utiliser un token unique (API key) pour chaque user qui se créera lors de la création de son compte

Limiter le nombre de requête à la seconde et retourné un code d'erreur HTTP 429 (too many request) 

<br>

Gérer les autorisations des ressources / verbes / propriété selon le rôle qu'à le user pour éviter qu'un user normal puisse supprimer un serveur alors qu'il n'est pas admin :

Utilisation de OAuth et JWT (JSON Web Token) pour gérer ces autorisations

Code HTTP retourné si le user n'a pas d'autorisations pour accéder aux différentes ressources ou les modifier : 401 (unauthorized)

<br><br>

## Whitelist : 
user normal :
- rejoindre un serveur
- envoyer un message au channel d'un des serveurs auquel il est inscrit
- créer un serveur (il en devient admin)
- quitter un serveur
- éditer / supprimer un message qu'il a lui-même envoyé

user admin :
- éditer / supprimer un serveur qu'il a créé
- créer / éditer / supprimer un channel sur un serveur qu'il a créé
- supprimer des messages d'autres utilisateurs sur un serveur qu'il a créé


Il n'a pas été mit en place ici, mais il aurait fallu mettre en plce un JWT de type Bearer Token qui contiendra l'information du fait que l'utilisateur est Administrateur. Et selon cela il aurait des endpooint qu'il pourra utiliser. 
Le Jeton permettra aussi de sécuriser l'API ou seul les personnes ayant un token valide pourra utiliser l'API.

