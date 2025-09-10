import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FoodList } from './food-list/food-list';
import { FoodDetails } from './food-details/food-details';
import { FoodCart } from './food-cart/food-cart';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,FoodList,FoodDetails,FoodCart],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('NewAngular');
}
