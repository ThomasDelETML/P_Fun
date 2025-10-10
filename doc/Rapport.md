# Rapport Projet Plot Those Lines!

**Élève :** Moreira Thomas  
**Client :** Xavier Carrel  
**Lieu :** ETML, Avenue de Valmont 28b, 1010 Lausanne  
**Dates :** 1er trimestre 2025  
**Auteur(s) :** Moreira Thomas  
**Version :** 1.1

---

## Table des matières
1. [Introduction](#introduction)  
2. [Description du domaine et sources de données](#description-du-domaine-et-sources-de-données)  
3. [Objectifs du projet](#objectifs-du-projet)  
4. [Analyse des besoins (User Stories)](#analyse-des-besoins-user-stories)  
5. [Planification](#planification)  
6. [Développement et architecture](#développement-et-architecture)  
7. [Tests et validation](#tests-et-validation)  
8. [Journal de travail](#journal-de-travail)  
9. [Usage de l’IA](#usage-de-lia)  
10. [Conclusion / Bilan](#conclusion--bilan)  
11. [Annexes](#annexes)  

---

## Introduction
Le projet **Plot Those Lines!** consiste à concevoir un logiciel permettant d’afficher des graphiques dynamiques à partir de fichiers CSV.  
Ce projet s’inscrit dans le cadre pédagogique de l’ETML et a pour objectif de développer des compétences techniques en C#, en visualisation de données et en structuration logicielle.  
Le produit final doit permettre à un utilisateur d’importer des données, de sélectionner un type de graphique (ligne, scatter ou barres), d’afficher les séries temporelles correspondantes et d’interagir avec les points affichés.

---

## Description du domaine et sources de données (min 250mots)
Le domaine étudié est la population du canton de Vaud et de la Romandie. Les données utilisées proviennent de sources officielles cantonales et fédérales. Elles sont fournies dans un format CSV, ce qui facilite leur importation et leur exploitation dans Visual Studio. La période couverte s’étend de 1803, année de l’indépendance vaudoise, jusqu’à 2024, offrant ainsi une vision démographique de plus de deux siècles. Les relevés sont effectués à intervalles réguliers d’environ dix ans : 1803, 1810, 1820, 1830 et ainsi de suite, jusqu’à l’époque contemporaine. Cette régularité garantit une cohérence temporelle utile pour toute analyse de tendance.

Les données ont été structurées pour être directement compatibles avec mon logiciel de visualisation de statistique. Chaque enregistrement comprend au minimum l’année du recensement et la population totale recensée. Cela permet d’élaborer facilement des graphiques linéaires, des histogrammes ou des autres. L’objectif principal de ce projet est de rendre l’évolution démographique plus claire, visuelle et intuitive, afin que toute personne, même non spécialiste, puisse comprendre comment la population a évolué au fil du temps.

Le choix de ce domaine repose sur plusieurs raisons solides : d’abord, la disponibilité de données publiques fiables issues d’institutions officielles. Ensuite, la présence d’évolutions temporelles nettes qui permettent d’identifier des périodes de croissance, de stagnation ou de déclin. Enfin, il s’agit d’un sujet personnellement motivant, car j’ai un intérêt particulier pour les statistiques et leur capacité à raconter une histoire à travers des chiffres.

---

## Objectifs du projet
### Objectifs fonctionnels
- Affichage graphique de séries temporelles multiples  
- Importation flexible des données  
- Affichage de fonctions et expressions personnalisées  

### Objectifs techniques
- Utilisation de LINQ  
- Création d’extensions C#  
- Choix des librairies graphiques et de présentation des données  

### Objectifs qualitatifs
- Code organisé, optimisé, commenté, testé  

### Objectifs pédagogiques (min 250mots)

### Objectifs produit (min 250mots)

---

## Analyse des besoins (User Stories)
- Présentation des User Stories avec tests d’acceptation  
- Maquettes ou diagrammes d’interface (PDF ou images)  
- Priorisation des fonctionnalités  

**Exemple de tableau :**

| ID   | User Story                                      | Critères d’acceptation                                | Priorité |
|------|-----------------------------------------------|-------------------------------------------------------|----------|
| US01 | En tant qu’utilisateur, je veux importer des fichiers CSV | Les fichiers CSV sont lus correctement et affichés   | Haute    |
| US02 | En tant qu’utilisateur, je veux afficher plusieurs séries temporelles | Les courbes apparaissent sur le même graphique       | Haute    |
| …    | …                                             | …                                                     | …        |

---

## Planification
- Planification globale (diagramme de Gantt ou tableau)  
- Découpage des tâches par User Story  
- Estimation du temps par tâche  
- Responsabilités et jalons  

---

## Développement et architecture
- Architecture générale du projet (diagramme UML ou schéma)  
- Description des namespaces et classes principales  
- Extensions C# créées et leur rôle  
- Choix techniques (librairies graphiques et traitement des données)  
- Gestion des erreurs et sécurité des données  

---

## Tests et validation
- Stratégie de tests (unitaires, intégration)  
- Résultats des tests (tableaux ou captures d’écran)  
- Bugs rencontrés et solutions appliquées  
- Vérification des critères d’acceptation  

---

## Bilans
### Bilan technique (min 250mots)

### Bilan personnel (min 250mots)

## Journal de travail
- Chronologie du projet avec activités réalisées  
- Difficultés rencontrées et solutions  
- Apports externes ou aides utilisées  
- Respect des deadlines  

---

## Usage de l’IA (min 250mots)
- Indiquer si une IA a été utilisée  
- Décrire le type d’assistance (ex: génération de scripts, tests, suggestions de code)  
- Explication précise de la manière dont l’IA a été utilisée et encadrée  

---

## Conclusion / Bilan
- Bilan général du projet  
- Points forts et limites  
- Enseignements tirés  
- Suggestions d’amélioration ou évolutions futures  

---

## Annexes
- Maquettes détaillées  
- Diagrammes UML complets  
- Exemple de fichiers de données utilisés  
- Copies d’écran du logiciel  
- Documentation complémentaire (si nécessaire)  
