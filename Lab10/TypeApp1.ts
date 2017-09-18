class BankAccount {
    money:number;
    constructor(money:number){this.money=money;}
    deposit(value: number){ this.money += value; }
}
// let myBankAccount: BankAccount={
//     money=2000,
//     deposit(value: number){ this.money += value; }
// }
let myBankAccount= new BankAccount(2000);

let myself:{name:string, bankAccount: BankAccount, hobbies:any[]}=
 {
    name:"Asaad",
    bankAccount: myBankAccount,
    hobbies:["Violin","Cooking"]
}

myself.bankAccount.deposit(3000);
console.log(myself);

export {}