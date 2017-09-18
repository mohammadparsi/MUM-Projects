class Person{
    private _firstName = "";
    set FirstName(value:string){
        this._firstName=value;
    }
    get FirstName(){
        return this._firstName;
    }
}

let person = new Person();

person.FirstName = "Asaad";
console.log(person.FirstName);