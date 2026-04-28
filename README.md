# BankTransferApp

A simple bank transfer application built with C# (.NET 10).  
It allows users to manage bank accounts, perform transactions, and transfer funds securely.

---

## Features

### User Management
- [x] User registration and authentication

### Bank Accounts
- [x] Create a new bank account  
- [x] Inactivate an existing bank account  
  - [ ] An account can only be inactivated if its balance is zero  
- [x] A user can have multiple accounts  
  - [x] Each account must have a **unique number per user**

### Account Types
- [x] Support for multiple account types:
  - **Checking Account** (US) / **Current Account** (UK) — _Conta corrente_
  - **Savings Account** — _Conta poupança_
  - **Payroll Account** — _Conta salário_  

### Transactions
- [x] Deposit funds into an account  
- [x] Withdraw funds from an account  
- [x] Transfer funds between user accounts  

### Account Information
- [ ] View account details  
- [ ] View transaction history  
- [ ] Track account balance updates in real time  
