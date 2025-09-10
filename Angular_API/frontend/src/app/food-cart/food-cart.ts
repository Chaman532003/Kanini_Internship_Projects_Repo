import { Component, computed, inject } from '@angular/core';
import { FoodService } from '../food.service';

@Component({
  selector: 'app-food-cart',
  imports: [],
  templateUrl: './food-cart.html',
  styleUrl: './food-cart.css'
})
export class FoodCart {
  foodservice = inject(FoodService)
// CartItems = computed(()=>this.foodservice.cart())
// total = computed(()=>this.foodservice.cart().reduce((sum,food)=>sum+food.price,0))
}
