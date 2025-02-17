# Système de Gestion des Transactions

## Description
Application console de gestion des transactions financières développée en .NET. Cette version permet aux utilisateurs de réaliser des opérations de crédit et de débit via une interface en ligne de commande.

## Diagramme de Classes du Core

```mermaid
classDiagram
    %% Models - Transactions
    class Transaction {
        <<abstract>>
        +decimal Amount
        +DateTime Date
        +TransactionType Type
        +Execute()
        +Validate()
    }

    class CreditTransaction {
        +Execute()
        +Validate()
    }

    class DebitTransaction {
        +Execute()
        +Validate()
    }

    %% Enums
    class TransactionType {
        <<enumeration>>
        CREDIT
        DEBIT
    }

    %% Validation Rules
    class IValidationRule {
        <<interface>>
        +Validate(Transaction transaction)
    }

    class MaximumCreditAmountRule {
        +Validate(Transaction transaction)
    }

    class MinimumBalanceRule {
        +Validate(Transaction transaction)
    }

    %% Services
    class ITransactionService {
        <<interface>>
        +ExecuteTransaction(Transaction transaction)
    }

    class TransactionServiceImpl {
        -List~IValidationRule~ _rules
        +ExecuteTransaction(Transaction transaction)
        -ValidateTransaction(Transaction transaction)
    }

    %% Factory
    class TransactionFactory {
        +CreateTransaction(TransactionType type, decimal amount)
    }

    %% Relations
    CreditTransaction --|> Transaction
    DebitTransaction --|> Transaction
    Transaction --> TransactionType
    MaximumCreditAmountRule ..|> IValidationRule
    MinimumBalanceRule ..|> IValidationRule
    TransactionServiceImpl ..|> ITransactionService
    TransactionServiceImpl --> IValidationRule
    TransactionFactory --> Transaction
    TransactionServiceImpl --> Transaction
```

## Flux de l'Application Console

```mermaid
sequenceDiagram
    participant User
    participant Console
    participant TransactionFactory
    participant TransactionService
    participant ValidationRules

    User->>Console: Saisie du type de transaction
    User->>Console: Saisie du montant
    Console->>TransactionFactory: CreateTransaction(type, amount)
    TransactionFactory-->>Console: Transaction
    Console->>TransactionService: ExecuteTransaction(transaction)
    TransactionService->>ValidationRules: Validate(transaction)
    ValidationRules-->>TransactionService: ValidationResult
    
    alt Transaction Valide
        TransactionService-->>Console: Success
        Console-->>User: Afficher confirmation
    else Transaction Invalide
        ValidationRules-->>TransactionService: ValidationError
        TransactionService-->>Console: Error
        Console-->>User: Afficher erreur
    end
```

## Installation

1. Prérequis :
   - .NET 8.0 ou supérieur
   - Visual Studio 2022 ou VS Code  

2. Clonez le dépôt :
```bash
git clone https://github.com/mdbouz89/TransactionManagement
```

3. Accédez au dossier du projet console :
```bash
cd TransactionManagement.Console
```

4. Restaurez les dépendances :
```bash
dotnet restore
```

5. Compilez le projet :
```bash
dotnet build
```

## Utilisation

1. Lancez l'application :
```bash
dotnet run
```

2. Menu Principal :
   - Choisissez le type de transaction (CREDIT/DEBIT)
   - Entrez le montant de la transaction
   - Suivez les instructions à l'écran

3. Règles de Validation :
   - Montant maximum pour les crédits : 10000
   - Solde minimum après débit : -5000
   - Montant minimum : 0.01

## Tests

Pour exécuter les tests :
```bash
cd TransactionManagement.Tests
dotnet test
```

## Fonctionnalités

- Création de transactions de crédit et débit
- Validation des transactions selon des règles métier
- Interface utilisateur en ligne de commande intuitive
- Gestion des erreurs et affichage des messages appropriés
- Historique des transactions effectuées