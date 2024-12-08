#####
Bank Management App
---
This console-based Bank Management App allows users to manage accounts and perform essential banking operations like viewing accounts, deposits, withdrawals, and transfers. The app uses JSON for storing and loading account data, ensuring persistent storage.

### **Features**:
- **Interactive Menu**: Use arrow keys to navigate and Enter to select options.
- **Account Management**: View account details and add new accounts.
- **Banking Operations**: Deposit, withdraw, and transfer money between accounts.
- **Transaction History**: View a summary of past transactions.
- **Data Persistence**: Save updated account information to the JSON file on exit.

---

### **Core Functions**:
1. **Load and Save Data**:
   - Reads account information from a JSON file at startup.
   - Updates and writes data back to the file when exiting.
2. **Interactive Navigation**:
   - Displays a dynamic menu with highlighted selections for a user-friendly experience.
3. **Account Details**:
   - Lists account type, number, and balance for all accounts.
4. **Banking Actions**:
   - **Deposit/Withdraw**: Prompt users for amounts and update balances.
   - **Transfer**: Facilitate money transfers by entering recipient details and amounts.
5. **Transaction History**:
   - Displays a log of previous banking activities for review.

---

### **How It Works**:
1. The app starts by loading data from a JSON file.
2. Users navigate through a colorful menu to choose banking operations.
3. Input prompts guide users for deposits, withdrawals, and transfers.
4. The app saves changes before exiting, ensuring all data is updated.

This app offers a simple yet effective way to manage bank accounts and perform transactions efficiently!
