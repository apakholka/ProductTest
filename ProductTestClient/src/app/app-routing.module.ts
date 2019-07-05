import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductRoutes } from './product/product-routing.module';
import { ProductsListComponent } from './product/products-list/products-list.component';

const routes: Routes = [
  { path: '', component: ProductsListComponent, pathMatch: 'full' },
  ...ProductRoutes,
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
