
//This routing is done by Nolawe Woldesemayat
import {Routes, RouterModule} from '@angular/router';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { HomeComponent } from './home/home.component';

const ROUTES: Routes = [
  { path: 'employees', component: EmployeeListComponent },
  { path: 'home',  component: HomeComponent },
  { path: '', redirectTo:'home', pathMatch: 'full'  }
]


export const MyRoutes = RouterModule.forRoot(ROUTES);
