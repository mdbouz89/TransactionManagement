Feature: Gestion des transactions
  En tant qu'utilisateur
  Je veux pouvoir gérer mes transactions financières
  Afin de suivre mon solde et mon historique

    Scenario: Traiter plusieurs transactions et valider le solde
    Given un solde initial de 1000
    When j'ajoute les transactions suivantes :
      | Type   | Amount | Description       |
      | Credit | 500    | Freelance Payment |
      | Debit  | 200    | Grocery Shopping  |
      | Debit  | 300    | Utility Bill      |
      | Credit | 1000   | Bonus             |
    Then le solde doit être de 2000
    And l'historique des transactions devrait contenir :
      | Type   | Amount | Description       |
      | Credit | 500    | Freelance Payment |
      | Debit  | 200    | Grocery Shopping  |
      | Debit  | 300    | Utility Bill      |
      | Credit | 1000   | Bonus             |

  Scenario: Ajouter une transaction de crédit qui dépasse le montant maximal
    Given un solde initial de 50
    When j'ajoute une transaction de type "Crédit" avec un montant de 12000 et une description "Salaire"
    Then la transaction doit échouer avec le message d'erreur :
     |  Error                                            |
     |  Le montant du crédit ne peut pas dépasser 10 000 |
    And le solde doit être de 50
    And l'historique des transactions devrait contenir :
      | Type   | Amount | Description       |

  Scenario: Ajouter une transaction de débit qui dépasse le découvert autorisé du solde
    Given un solde initial de 500
    When j'ajoute une transaction de type "Débit" avec un montant de 6000 et une description "Loyer"
    Then la transaction doit échouer avec le message d'erreur :
     | Error                                        |
     | Le solde ne peut pas être inférieur à -5 000 |
    And le solde doit rester à 500
