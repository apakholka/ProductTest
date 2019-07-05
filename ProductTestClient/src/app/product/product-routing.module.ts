import { Routes } from '@angular/router';
import { ProductsListComponent } from './products-list/products-list.component';
import { ProductDetailsComponent } from './product-details/product-details.component';

export const ProductRoutes : Routes = [
  { path: 'product_table', component: ProductsListComponent},
  { path: "product_details", component: ProductDetailsComponent},
];