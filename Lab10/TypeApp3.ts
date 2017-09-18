class Base {
    public width: number = 0;
    length: number = 0;


}

class Rectangle extends Base {
    calcSize() { return this.width * this.length }
}

let rec = new Rectangle();
rec.width = 5;
rec.length = 2;

console.log(rec.calcSize());