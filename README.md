# Unity ECS - Simulation de Trafic & Système de Waypoints

Ce projet est un prototype de simulation de trafic développé sur **Unity** afin d'expérimenter et de prendre en main l'architecture **ECS (Entity Component System)** et le package **DOTS (Data-Oriented Technology Stack)**.

L'objectif était de concevoir un système performant de génération et de déplacement de véhicules suivant un itinéraire dynamique.

## 🛠️ Fonctionnalités Implémentées

### Étape 1 : Déplacement de base (Point & Click)

- Implémentation d'une approche "Point & Click" : l'utilisateur clique dans la scène, et tous les véhicules dotés du composant `CarMoverAuthoring` se déplacent automatiquement vers cette coordonnée.

### Étape 2 : Spawner de véhicules paramétrable

- Création d'un spawner configurable directement depuis l'éditeur d'Unity.
- Personnalisation dynamique des véhicules lors de leur apparition : vitesse, fréquence de spawn, et application d'une couleur aléatoire parmi un ensemble prédéfini.
- Sélection aléatoire du modèle de véhicule parmi trois prefabs différents, transférant toutes les propriétés du spawner à l'entité générée.

### Étape 3 : Système de Road/Waypoints performant en ECS

- Conception d'un système de navigation par points de passage (Waypoints) pour tracer une route.
- Utilisation d'un composant _Authoring_ pour récupérer automatiquement les positions de GameObjects cibles afin de construire la route dynamiquement.
- Intégration d'`IBufferElementData` et de `DynamicBuffer` pour stocker efficacement la collection de positions directement sur l'entité, garantissant une flexibilité et des performances optimales propres à l'architecture ECS.
- Refonte du système de déplacement pour que les véhicules générés suivent fidèlement la route et ses différents points de passage.

## 🧰 Technologies & Packages utilisés

- **Unity**
- **Unity ECS / Entities** (`Entities`, `Entities Graphics`)
- Programmation Orientée Données (DOP)

## 📖 Ressources utiles

- [Introduction à Unity ECS (YouTube)](https://www.youtube.com/watch?v=1gSnTlUjs-s)
- Forums Unity & Stack Overflow
