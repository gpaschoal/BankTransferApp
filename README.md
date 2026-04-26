# BankTransferApp

A simple bank transfer application built with C# (.NET 10).  
It allows users to manage bank accounts, perform transactions, and transfer funds securely.

---

## Features

### User Management
- [x] User registration and authentication

### Bank Accounts
- [ ] Create a new bank account  
- [ ] Inactivate an existing bank account  
  - An account can only be inactivated if its balance is zero  
- [ ] A user can have multiple accounts  
  - Each account must have a **unique name per user**  
  - Different users may have accounts with the same name  

### Account Types
- [ ] Support for multiple account types:
  - **Checking Account** (US) / **Current Account** (UK) — _Conta corrente_
  - **Savings Account** — _Conta poupança_
  - **Payroll Account** — _Conta salário_  

### Transactions
- [ ] Deposit funds into an account  
- [ ] Withdraw funds from an account  
- [ ] Transfer funds between user accounts  

### Account Information
- [ ] View account details  
- [ ] View transaction history  
- [ ] Track account balance updates in real time  

## Notes

- Account name uniqueness is enforced **per user**, not globally  
- Business rules (e.g., zero balance for inactivation) are strictly validated  
- Designed with scalability and clean architecture principles in mind  