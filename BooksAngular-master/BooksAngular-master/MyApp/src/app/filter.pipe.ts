//unused
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any, args: any) {

    console.log("value "+value+" args "+args)
    return false || args.includes(value);
  }

}
