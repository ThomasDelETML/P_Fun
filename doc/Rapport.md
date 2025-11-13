# Rapport Projet Plot Those Lines!

**Élève :** Moreira Thomas  
**Client :** Xavier Carrel  
**Lieu :** ETML, Avenue de Valmont 28b, 1010 Lausanne  
**Dates :** 1er trimestre 2025  
**Auteur(s) :** Moreira Thomas  
**Version :** 1.2

---

## Table des matières
1. [Introduction](#introduction)  
2. [Description du domaine et sources de données](#description-du-domaine-et-sources-de-données)  
3. [Objectifs du projet](#objectifs-du-projet)  
4. [Analyse fonctionnel](#analyse-fonctionnel)  
5. [Planification](#planification)  
6. [Schéma et architecture](#schéma-et-architecture)  
7. [Tests et validation](#tests-et-validation)
8. [Bilans](#bilans)  
9. [Journal de travail](#journal-de-travail)  
10. [Usage de l’IA](#usage-de-lia)  
12. [Annexes](#annexes)  

---

## Introduction
Le projet **Plot Those Lines!** consiste à concevoir un logiciel permettant d’afficher des graphiques dynamiques à partir de fichiers CSV.  
Ce projet s’inscrit dans le cadre pédagogique de l’ETML et a pour objectif de développer des compétences techniques en C#, en visualisation de données et en structuration logicielle.  
Le produit final doit permettre à un utilisateur d’importer des données, de sélectionner un type de graphique (ligne, scatter ou barres), d’afficher les séries temporelles correspondantes et d’interagir avec les points affichés.

---

## Description du domaine et sources de données
Le domaine étudié est la population du canton de Vaud et de la Romandie. Les données utilisées proviennent de sources officielles cantonales et fédérales. Elles sont fournies dans un format CSV, ce qui facilite leur importation et leur exploitation dans Visual Studio. La période couverte s’étend de 1803, année de l’indépendance vaudoise, jusqu’à 2024, offrant ainsi une vision démographique de plus de deux siècles. Les relevés sont effectués à intervalles réguliers d’environ dix ans : 1803, 1810, 1820, 1830 et ainsi de suite, jusqu’à l’époque contemporaine. Cette régularité garantit une cohérence temporelle utile pour toute analyse de tendance.

Les données ont été structurées pour être directement compatibles avec mon logiciel de visualisation de statistique. Chaque enregistrement comprend au minimum l’année du recensement et la population totale recensée. Cela permet d’élaborer facilement des graphiques linéaires, des histogrammes ou des autres. L’objectif principal de ce projet est de rendre l’évolution démographique plus claire, visuelle et intuitive, afin que toute personne, même non spécialiste, puisse comprendre comment la population a évolué au fil du temps.

Le choix de ce domaine repose sur plusieurs raisons solides : d’abord, la disponibilité de données publiques fiables issues d’institutions officielles. Ensuite, la présence d’évolutions temporelles nettes qui permettent d’identifier des périodes de croissance, de stagnation ou de déclin. Enfin, il s’agit d’un sujet personnellement motivant, car j’ai un intérêt particulier pour les statistiques et leur capacité à raconter une histoire à travers des chiffres.

---

## Objectifs du projet
### Objectifs fonctionnels
- Affichage graphique de séries temporelles multiples  
- Importation flexible des données

### Objectifs techniques
- Utilisation de LINQ  
- Utilisation d’extensions C#  
- Choix des librairies graphiques et de présentation des données  

### Objectifs qualitatifs
- Code organisé, optimisé, commenté, testé  

### Objectifs pédagogiques
L’objectif pédagogique principal de ce projet est d’approfondir mes compétences en programmation  C#, tout en appliquant des concepts avancés tels que LINQ et la programmation fonctionnel. Le projet Plot Those Lines! ma permit de passer de la simple écriture de code à la conception d’une application complète, structurée et maintenable. L’enjeu n’est donc pas uniquement de produire un programme fonctionnel, mais aussi de comprendre les principes de conception logicielle nécessaires à un développement professionnel.

Un autre objectif est d’apprendre à analyser un besoin client, à le traduire en fonctionnalités concrètes et à le planifier sous forme de User Stories. Ce travail m’enseigne la rigueur nécessaire à la communication entre le développeur et le client (bien que ce client était moi-même), ainsi que l’importance d’une documentation claire et structurée. La gestion des priorités et la planification dans le temps, participent également à la compréhension du cycle de vie complet d’un projet logiciel.

Sur le plan technique, le projet me pousse à découvrir et maîtriser des bibliothèques spécialisées dans la visualisation de données et à comprendre comment manipuler efficacement des fichiers CSV, gérer les exceptions et assurer la robustesse du code. L’utilisation de tests unitaires fait aussi partie de la formation afin de garantir la fiabilité des fonctions développées.

Enfin, le projet a une visée réflexive : il s’agit d’apprendre à évaluer son propre travail, à identifier les points à améliorer et à tirer des leçons des erreurs rencontrées. En résumé, ce projet me permet de combiner compétences techniques, méthodologie de projet et autonomie, trois éléments essentiels pour évoluer vers une pratique professionnelle du développement logiciel.

### Objectifs produit
L’objectif produit du projet **Plot Those Lines!** est de concevoir un logiciel capable de transformer des fichiers de données brutes au format CSV en graphiques clairs, dynamiques et personnalisables. Le produit final doit offrir une interface intuitive permettant à tout utilisateur, qu’il soit novice ou expérimenté, d’importer ses propres jeux de données et de les visualiser sous forme de courbes ou de diagrammes à barres. L’idée centrale est de rendre la lecture et l’interprétation de données numériques plus visuelles et accessibles, tout en conservant une approche rigoureuse et précise.

Le logiciel doit permettre d’ajuster le type de graphique souhaité et d’afficher plusieurs séries sur un même plan pour faciliter les comparaisons. Il inclut également la possibilité d’interagir avec les graphiques, par exemple en affichant les valeurs exactes au survol. Une attention particulière est portée à la lisibilité : axes clairs, légendes et couleurs distinctes.

Sur le plan technique, l’objectif est de garantir un fonctionnement fluide et stable, même avec des fichiers volumineux. L’application doit gérer les erreurs d’importation (valeurs manquantes, séparateurs incohérents) et offrir des messages d’erreur explicites. Le code est conçu pour être extensible, afin de pouvoir ajouter facilement de nouveaux types de visualisations ou formats de données à l’avenir.

En somme, **Plot Those Lines!** vise à fournir un outil polyvalent et robuste, combinant efficacité technique, simplicité d’utilisation et pertinence visuelle. Ce produit répond à un besoin réel : permettre une compréhension rapide et graphique de données statistiques sans nécessiter de compétences en programmation ou en traitement de données.

---

## Analyse fonctionnel

**User stories :**

| Nom   | User Story                                      | Critères d’acceptation                                | Lien |
|------|-----------------------------------------------|-------------------------------------------------------|----------|
| Design de l'app | En tant que developpeur, je souhaite pouvoir avoir une mockup de l'app avant de la réaliser. | [x] La mockup est réaliser et compréhensible | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=127572113&issue=ThomasDelETML%7CP_Fun%7C6) |
|  |  | [x] Il y le lien pour la mockup a P_Fun/doc/Figma |  |
| Création de la base de code | En tant que developpeur, je veux crée la base du code, n'ayant pas encore tout le savoir necessaire a crée tout pour l'app. | [x] Le fichier est crée | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=127575059&issue=ThomasDelETML%7CP_Fun%7C7) |
|  |  | [x] La base graphique & de code est réaliser |  |
| Affichage simultané de plusieurs séries temporelles | En tant qu’utilisateur, je veux afficher une représentation graphique de plusieurs séries temporelles simultanément. | [x] Lorsque des données sont insérées dans le programme avec "Import Chart", une ligne est tracée pour chaque entrée de données, représentant correctement les données. | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=127575059&issue=ThomasDelETML%7CP_Fun%7C7) |
|  |  | [x] Lorsque les données sont tracées, la période temporelle de toutes les données est la même. |  |

<img width="1121" height="799" alt="image" src="https://github.com/user-attachments/assets/f6b49f5f-0d6b-41fe-9a1e-94fd8556a436" />

On cherche a montrer les parametres d'affichage. On montre que le graphique doit avoire un axe x et y, et que il y a une legende avec la couleur et le nom d'une donné.

| Nom   | User Story                                      | Critères d’acceptation                                | Lien |
|------|-----------------------------------------------|-------------------------------------------------------|----------|
| Options flexibles d’affichage et d’analyse | En tant qu’utilisateur, je veux bénéficier d’une grande flexibilité d’affichage afin de pouvoir analyser mes données en détail. | [x] Pendant que l’application est en cours d’exécution, lorsque je zoom, l’application effectue un zoom selon la position du curseur. | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542641&issue=ThomasDelETML%7CP_Fun%7C2) |
|  |  | [x] Pendant que l’application est en cours d’exécution et que les données sont correctement importées, une échelle est affichée sur les axes X et Y afin d’obtenir une meilleure perspective des données. |  |
|  |  | [x] Pendant que l’application est en cours d’exécution et que les données sont correctement importées, un texte représentant chaque entrée de données est affiché dans la couleur dans laquelle elle est tracée sur le graphique. |  |
|  |  | [x] Lorsque je survole un point précis des données, je vois la valeur exacte de l’entrée pour la date correspondante. |  |
|  |  | [x] Lorsque les données sont tracées, en cliquant sur le menu déroulant "Choose the chart type", les données sont formatées selon le type de graphique choisi (graphique en ligne, histogramme, etc.). |  |
|  |  | [x] Lorsque les données sont tracées, en utilisant la zone de texte "Name of the chart", l’utilisateur peut saisir et modifier un titre et l’afficher sur le graphique. |  |


<img width="1127" height="800" alt="image" src="https://github.com/user-attachments/assets/2dbbd2ae-4c0c-4e25-a398-91d99f80732c" />

On cherche a montrer que il y a un affichage de la date et de la valeur d'une donné si la souris et mis sur une donne.

| Nom   | User Story                                      | Critères d’acceptation                                | Lien |
|------|-----------------------------------------------|-------------------------------------------------------|----------|
| Importation permanente de séries temporelles | En tant qu’utilisateur, je veux importer des séries de données de manière permanente. Le programme me permet d’importer un ou plusieurs formats de données. | [x] Lors de la première ouverture du programme, aucun graphique n’est affiché. | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542752&issue=ThomasDelETML%7CP_Fun%7C3) |
|  |  | [x] Lors des ouvertures suivantes, le graphique affiché précédemment est de nouveau affiché. |  |
|  |  | [x] Pendant que l’application est en cours d’exécution, lorsque j’importe un fichier CSV avec "Import Chart", les données sont correctement importées et fonctionnent. Si les données contiennent une erreur (mauvais type de valeur, valeur impossible, valeur vide) ou aucun changement (dans les en-têtes et les données), le programme n’importe pas les données et informe l’utilisateur du problème rencontré. |  |
|  |  | [x] Pendant que l’application est en cours d’exécution, si j’essaie d’importer des données qui existent déjà, le programme n’importe pas ces données et informe l’utilisateur que les données existent déjà. |  |

<img width="1130" height="799" alt="image" src="https://github.com/user-attachments/assets/190c0218-1d06-451e-994b-5d9c615bfd6e" />

On cherche a montrer qu'il y a un bouton pour importer et comment les donnés sont stockés de manières permanentes. Si les donnes sont validés par le programme, le fichier est copié dans le repertoire du programme et ensuite chargé pour etre affiché.

| Nom   | User Story                                      | Critères d’acceptation                                | Lien |
|------|-----------------------------------------------|-------------------------------------------------------|----------|
| Affichage de données sur plusieurs intervalles de temps consécutifs | En tant qu’utilisateur, je souhaite afficher plusieurs intervalles de temps pour les mêmes données. | [x] Lorsqu’un jeu de données est tracé et qu’il contient plusieurs séries temporelles pour une seule entrée, le programme affiche les deux entrées consécutives comme une seule ligne. | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542903&issue=ThomasDelETML%7CP_Fun%7C4) |


---

## Planification
- Pour la planification j'ai fais toutes les User Stories avant de toucher le code. De cet manières j'avais une meilleur visualisation du projet sur le temps.

---

## Schéma et architecture
- Voici le schéma de mon application. On peut y voir l'importation des données.

<img width="867" height="868" alt="image" src="https://github.com/user-attachments/assets/e756bb79-f81b-4416-9dd2-e584607c268e" />

- L'user prend un fichier .csv n'importe ou sur sont pc, le programme prend ce fichier, le copie dans *Plot thoses lines !\bin\Debug\data.csv* et l'utilise selon l'utilisation de l'application. Quand l'user prend un autre fichier d'un endroit de son ordinateur, l'ancien fichier enregistrer sur *Plot thoses lines !\bin\Debug\data.csv* est remplacer.

---

## Tests et validation
| User story | Criteres d acceptation (resume) | Statut | Lien issue |
|---|---|---:|---|
| Meme periode pour toutes les series | Toutes les series partagent le meme axe temporel / meme plage X lors de l affichage | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542433&issue=ThomasDelETML%7CP_Fun%7C1) |
| Zoom | Molette effectue un zoom centre et fluide de l affichage | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542641&issue=ThomasDelETML%7CP_Fun%7C2) |
| Echelles visibles | Axes X et Y visibles des que des donnees sont importeess | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542641&issue=ThomasDelETML%7CP_Fun%7C2) |
| Labels colores | Legende affiche un label par serie avec couleur correspondante | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542641&issue=ThomasDelETML%7CP_Fun%7C2) |
| Valeur au survol | Au survol d'un point, affichage du nom de serie, X et Y exacts | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542641&issue=ThomasDelETML%7CP_Fun%7C2) |
| Titre personnalise | Saisie d'un titre et application immediate | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542641&issue=ThomasDelETML%7CP_Fun%7C2) |
| Aucun graphique au premier lancement | Sans fichier data.csv, aucun trace initial | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542752&issue=ThomasDelETML%7CP_Fun%7C3) |
| Restauration du graphique precedent | Si data.csv present, recharge a l'ouverture suivante | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542752&issue=ThomasDelETML%7CP_Fun%7C3) |
| Import robuste & gestion d'erreurs | Types stricts, pas de vides, pas de NaN/Inf, messages explicites | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542752&issue=ThomasDelETML%7CP_Fun%7C3) |
| Pas d'import en double | Fichier identique ou donnees identiques detectees et refusees | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542752&issue=ThomasDelETML%7CP_Fun%7C3) |
| Changement de type de graphique | Selection Line/Scatter/Bar change le rendu sans erreur | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542641&issue=ThomasDelETML%7CP_Fun%7C2) |
| Fusion des datasets | Union des X triee, nouvelles valeurs ecrasent les anciennes aux memes X | OK | [Lien](https://github.com/users/ThomasDelETML/projects/4/views/1?pane=issue&itemId=126542903&issue=ThomasDelETML%7CP_Fun%7C4) |

---

## Bilans
### Bilan technique
Le bilan technique de ce projet est globalement positif. Le développement a permis de mettre en place une architecture cohérente de gestion de données. L’application ne se contente pas d’afficher des données importées d’un CSV, elle valide strictement les données, les fusionne avec un dataset existant, puis les affiche dynamiquement avec différents modes de visualisation (Line, Scatter, Bar). Cela démontre un vrai travail de programmation et non uniquement un travail visuel.

Le projet a permis de développer des compétences sur la gestion de données à partir de sources CSV. La partie fusion des datasets est techniquement importante car elle permet de synchroniser plusieurs imports successifs sur un même axe X, qu’ils aient exactement les mêmes années ou non, ce qui rend l’application utile dans un contexte réel où plusieurs fichiers CSV ne sont jamais parfaitement alignés.

La partie visualisation avec ScottPlot a permis de comprendre les mécaniques d’interaction utilisateur et de rendu graphique performant dans un environnement C# forms classique. Le survol de souris pour récupérer la valeur exacte du point montre une maitrise correcte de la gestion d’évènements et du mapping.

Enfin, ce projet répond à toutes les demande dans le cahier des charges. Toutes les User stories on était réaliser et le code et clair et fonctionnel.

### Bilan personnel
Durant ce projet j’ai apprécié pouvoir travailler sur un sujet que j’ai pu choisir moi-même. Le fait d’utiliser des données réelles sur la population romande était motivant parce que je pouvais visualiser concrètement ce que je manipulais et je pouvais voir directement la valeur et l’intérêt des représentations graphiques. Ce choix libre m’a donné un sentiment de contrôle sur ce que je produisais.

D’un point de vue personnel, j’ai l’impression d’avoir fait de mon mieux et je suis satisfait du résultat final. Chaque étape que j’ai dû corriger ou améliorer m’a fait progresser techniquement. J’ai dû faire face à plusieurs petites difficultés liées à WinForms, à ScottPlot, et aux validations CSV, et pourtant j’ai réussi à aller jusqu’au bout. Cette progression m’a montré que je pouvais tenir un projet complet, allant du design jusqu’à une version stable et propre.

Le fait d’avoir réussi à construire une application qui charge, fusionne, valide et trace des données réelles est quelque chose de concret pour moi. Je suis content d’avoir atteint ce niveau de qualité et d’avoir pu intégrer plusieurs concepts différents dans un seul outil fonctionnel: import, traitement, fusion, visualisation, contrôle utilisateur. Ce projet me donne confiance pour la suite, parce que j’ai dépassé un exercice scolaire “simple” et j’ai réalisé une application qui a du sens et de l’utilité potentielle dans le monde réel.

## Journal de travail
- Journal de travail réaliser avec GitJournal.

---

## Usage de l’IA
Durant ce projet, j’ai utilisé l’intelligence artificielle uniquement dans des contextes où elle apportait un gain réel et où l’intervention humaine n’apportait pas de valeur supplémentaire. Par exemple, je l’ai utilisée pour produire un squelette de rapport ou pour générer des données CSV brutes qui me servaient uniquement de matière de test. Dans ces deux cas, ce sont des tâches répétitives, longues, et qui ne nécessitent pas de réflexion humaine particulière, seulement de la production mécanique. Donc l’IA a permis de me concentrer davantage sur la partie centrale du projet: la conception logicielle, la logique interne, les validations et la qualité du code.

J’ai également utilisé l’IA quand j’étais bloqué sur des problèmes spécifiques de programmation, pour valider une direction technique ou pour débloquer un point où je ne trouvais pas la solution dans les sources conventionnelles (StackOverflow, documentation, youtube, etc.). L’objectif n’était pas de demander à l’IA de coder entièrement à ma place, mais plutôt de m’aider à comprendre, corriger, ou confirmer une approche. Les changements de code ont été écrits et intégrés par moi, et je n’ai pas demandé à l’IA de générer des morceaux complets de code sans contrôle. Le rôle était: assistance ciblée, pas délégation complète.

Enfin, j’ai utilisé l’IA aussi pour la partie rédaction, surtout pour corriger des erreurs d’orthographe, de formulation, ou pour reformuler certains passages dans un français plus clair, plus professionnel, mieux structurée. Cela m’a permis d’améliorer la qualité rédactionnelle du rapport général, sans que cela modifie mon contenu et sans altérer mes idées, mes réflexions ou mon analyse personnelle. Donc l’IA a eu une fonction d’optimisation, pas de substitution.

Aussi, si vous pensez que cela a été généré par une IA parce que le français est trop « bon » pour moi, je souhaite vous rappeler que je suis né en Suisse et que j’y ai fait toute ma scolarité. Ce n’est pas parce que je ne parle pas en français élaboré à mes collègues que je n’ai pas les compétences de le faire.

---

## Annexes
- Tout fichiers binaire dans P_Fun/doc
- Contactez-moi si besoins po51oro@eduvaud.ch

---

# Moreira Thomas, 13.11.2025
