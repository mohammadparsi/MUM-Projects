var Person = /** @class */ (function () {
    function Person() {
        this._firstName = "";
    }
    Object.defineProperty(Person.prototype, "FirstName", {
        get: function () {
            return this._firstName;
        },
        set: function (value) {
            this._firstName = value;
        },
        enumerable: true,
        configurable: true
    });
    return Person;
}());
var person = new Person();
person.FirstName = "Asaad";
console.log(person.FirstName);
//# sourceMappingURL=TypeApp4.js.map