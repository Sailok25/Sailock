# Changelog — Sailock
Tous les changements de Sailock sont documentés ici.

## [1.5.0] - 2026-07-16

### Ajouté
* Nouvel assistant d'importation avec les modes Fusionner, Remplacer et Personnalisé
* Gestion configurable des doublons lors de l'importation (Conserver, Écraser ou Renommer)
* Nouvelles colonnes Date de création et Date de modification dans le tableau de bord
* Localisation complète de toutes les valeurs des listes déroulantes des Paramètres (anglais, espagnol, allemand et français)

### Modifié
* Processus d'importation repensé avec une confirmation avant l'application des modifications
* Le tableau de bord utilise désormais un menu contextuel à trois points au lieu du bouton Modifier
* Amélioration de la disposition du tableau de bord et du format des dates
* L'écran de connexion démarre désormais toujours avec le thème sombre et applique le thème sélectionné après le déverrouillage du coffre
* Les textes de l'interface utilisent désormais une capitalisation cohérente
* Suppression de la fenêtre du journal des modifications intégrée ; les notes de version sont désormais disponibles dans le dépôt GitHub

### Corrigé
* Couleurs plus douces dans le thème clair pour améliorer le confort visuel
* Correction de la visibilité de l'étiquette du verrouillage automatique dans le thème clair
* Correction de la couleur du texte descriptif du verrouillage automatique dans les Paramètres
* Correction des traductions manquantes dans la boîte de dialogue Modifier le mot de passe principal
* Correction de la persistance du thème au démarrage de l'application

---

## [1.4.2] - 2026-07-01

### Fixed
* Correction du problème de cache lors de la mise à jour du mot de passe principal

---

## [1.4.1] - 2026-06-28

### Fixed
* Ajout des traductions anglaise, espagnole, allemande et française dans la fenêtre de modification du mot de passe principal

---

## [1.4.0] - 2026-06-21

### Added
* Nouveau filtre de catégories dans le tableau de bord pour afficher les entrées par catégorie ou toutes les entrées.
* Champ URL/site web dans les entrées de mot de passe pour identifier plus facilement les comptes.
* Prise en charge de la langue allemande (Deutsch).
* Prise en charge de la langue française (Français).

### Changed
* Le tableau de bord affiche désormais une colonne Site web avec l'URL associée à chaque entrée.
* La recherche inclut désormais les correspondances dans les URLs et sites web.
* La section langue des paramètres inclut de nouvelles options de localisation.

---

## [1.3.1] - 2026-06-15

### Added
* Durée de verrouillage automatique configurable : 30 secondes, 1 minute, 2 minutes, 5 minutes, ou entièrement désactivée

### Changed
* Le sélecteur de verrouillage automatique dans les paramètres est désormais une liste déroulante, comme celui de la langue ou de la taille du texte
* La description du verrouillage automatique dans les paramètres est désormais neutre

---

## [1.3.0] - 2026-06-08

### Added
* Thème clair complet implémenté dans toute l'application
* Changement de mot de passe principal fonctionnel avec validations de sécurité
* Barres de défilement unifiées dans toute l'application avec un style cohérent
* Effet de survol amélioré sur les boutons de fenêtre (réduire, agrandir, fermer)

### Changed
* Les barres de défilement ont désormais le même style visuel dans le tableau de bord, les paramètres, le générateur et l'historique des modifications
* L'interface des paramètres utilise désormais un ScrollViewer pour une meilleure navigation sur petits écrans
* Barre latérale améliorée avec un survol plus visible en mode clair
* Badge Latest/Legacy dans l'historique des modifications avec des couleurs personnalisables
* Le ScrollViewer du tableau de bord se trouve désormais en dehors du tableau pour une meilleure expérience

### Fixed
* Les textes blancs en mode clair sont désormais visibles (noir/vert foncé)
* Cases à cocher et interrupteurs désormais visibles en mode clair
* Boutons de fenêtre avec survol plus visible dans les deux thèmes
* La barre de défilement du générateur apparaît désormais lorsque la fenêtre est réduite
* Les badges de l'historique des modifications ont désormais un contraste adéquat

---

## [1.2.2] - 2026-06-05

### Fixed
* Visibilité du bouton de version dans la barre latérale restaurée
* Correction d'un problème rendant l'accès à l'historique des versions peu visible

---

## [1.2.1] - 2026-06-04

### Fixed
* Correction de la désynchronisation entre le champ de mot de passe visible et masqué
* Résolution du problème de cache visuel dans le champ du mot de passe principal
* Correction de l'alignement du champ de mot de passe lors du basculement de la visibilité
* Correction des fautes d'orthographe dans les textes en espagnol

### Changed
* Police mise à jour vers JetBrains Mono
* Ajustement des tailles Small, Default et Large avec une différence plus marquée
* La mise à l'échelle n'affecte plus les fenêtres modales ni les formulaires
* La barre latérale s'adapte désormais avec le contenu

---

## [1.2.0] - 2026-06-01

### Added
* Prise en charge complète de l'application ajoutée pour : l'anglais et l'espagnol
* La sélection de la langue est désormais disponible dans les paramètres et est conservée d'une session à l'autre
* Toutes les vues, fenêtres modales et messages système ont été entièrement traduits

### Changed
* Amélioration du style des barres de défilement dans la vue de l'historique des modifications
* Le bouton de version de la barre latérale utilise désormais un style cohérent avec l'écran de connexion

### Fixed
* Correction d'un problème empêchant l'affichage correct de certains textes de l'interface
* Correction d'un problème du système de langues pouvant empêcher l'affichage correct de certaines traductions

---

## [1.1.3] - 2026-05-30

### Added
* Ajout de la vérification en deux étapes (2FA) pour plus de sécurité à la connexion
* Vous pouvez désormais confirmer la désactivation du 2FA via un message de sécurité
* Une confirmation du mot de passe est désormais requise avant de modifier un mot de passe enregistré
* Ajout d'une option pour afficher ou masquer les mots de passe dans les fenêtres de modification
* Vous pouvez désormais modifier la taille de l'interface (petite, normale ou grande)
* Ajout du mode clair en plus du mode sombre
* Vous pouvez désormais ajuster le contraste visuel de l'application pour une meilleure lisibilité
* La fenêtre d'ajout d'un nouveau mot de passe est désormais plus simple et plus rapide à utiliser
* La fenêtre de modification n'affiche désormais que les options pertinentes selon les modifications effectuées

### Changed
* Amélioration de l'expérience générale de modification des mots de passe
* Optimisation de l'affichage des éléments dans toute l'application

### Fixed
* Correction d'un problème où la fenêtre d'ajout de mot de passe ne se fermait pas correctement
* Correction du logo dupliqué dans la barre latérale
* Le champ du code de vérification est désormais correctement centré
* Suppression d'une ligne visuelle inutile en haut de l'application
* Amélioration du chargement du logo dans toute l'interface

---

## [1.1.2] - 2026-05-28

### Added
* Vous pouvez désormais importer vos données depuis un fichier de sauvegarde
* Vous pouvez désormais exporter vos données en toute sécurité vers un fichier .slock
* Ajout de l'option de suppression de toutes les données de l'application
* Ajout du verrouillage automatique de l'application après une période d'inactivité
* Messages de confirmation lors de l'import ou de l'export de données

### Fixed
* Correction d'un problème empêchant le chargement correct des données importées
* Correction d'une erreur lors de l'export des données dans certains cas

---

## [1.1.1] - 2026-05-27

### Added
* Nouvel écran de paramètres regroupant toutes les options principales :
  * Sécurité (2FA, changement de mot de passe, verrouillage automatique)
  * Apparence (thème clair/sombre, contraste, taille du texte)
  * Gestion des données (import et export)
  * Zone de danger (suppression complète avec confirmation)
* L'application peut désormais basculer entre le thème clair et sombre
* Ajout de la prise en charge de l'ajustement de la taille de l'interface
* Préparation du système pour de futures améliorations visuelles

### Changed
* Amélioration de la fiabilité de l'enregistrement des données par l'application
* La connexion utilise désormais le mot de passe réel de l'utilisateur
* Lors du premier lancement, l'utilisateur peut créer son mot de passe principal via un parcours guidé

### Fixed
* Correction de problèmes de stabilité lors de l'enregistrement des données
* Correction d'erreurs de navigation entre les écrans

---

## [1.0.2] - 2026-05-25

### Added
* Panneau principal permettant de voir tous les mots de passe enregistrés
* Fenêtre pour ajouter facilement de nouveaux mots de passe
* Fenêtre pour modifier et supprimer des mots de passe existants
* Générateur de mots de passe avec options de personnalisation
* Navigation entre les sections (accueil, générateur et paramètres)
* Boutons de fenêtre personnalisés (réduire, agrandir et fermer)
* Design de base complet de l'application
* Numéro de version visible dans l'application

### Changed
* Amélioration de la navigation entre les sections pour un parcours plus fluide
* Renforcement de la stabilité générale de l'application

### Fixed
* Correction du système de connexion
* Correction de problèmes de réactivité des boutons et éléments de l'interface

---

## [1.0.0] - 2026-05-23

### Added
* Écran de connexion
* Structure initiale de l'application
* Design de base de tous les écrans principaux
* Système de navigation entre les sections
* Logo et style visuel initiaux
