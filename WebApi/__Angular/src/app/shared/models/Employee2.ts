import { Injectable, Input } from '@angular/core';


@Injectable()
export class Employee2 {
    public Id: string = '';
    public FirstName: string = '';
    public LastName: string = '';
    public FICAPercentage: number = 0;
    public TaxesPercentage: number = 0;
    public PostTaxPercentage: number = 0;
    public PreTaxPercentage: number = 0;
    private Salary: number;
    public NetPay: number;


    constructor() {}

        
   public calculateNetPay() {
        this.NetPay = (100 - (this.TaxesPercentage +
            this.FICAPercentage +
            this.PostTaxPercentage +
            this.PreTaxPercentage
        )) * this.Salary / 100;
    }

    
    // get Salary():number {
    //     return this.Salary;
    // }
    // @Input()set Salary(salary:number) {
    //     this.salary = salary;
    // }
}

