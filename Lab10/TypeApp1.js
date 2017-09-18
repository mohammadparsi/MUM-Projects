"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var BankAccount = /** @class */ (function () {
    function BankAccount(money) {
        this.money = money;
    }
    BankAccount.prototype.deposit = function (value) { this.money += value; };
    return BankAccount;
}());
// let myBankAccount: BankAccount={
//     money=2000,
//     deposit(value: number){ this.money += value; }
// }
var myBankAccount = new BankAccount(2000);
var myself = {
    name: "Asaad",
    bankAccount: myBankAccount,
    hobbies: ["Violin", "Cooking"]
};
myself.bankAccount.deposit(3000);
console.log(myself);
//# sourceMappingURL=TypeApp1.js.map