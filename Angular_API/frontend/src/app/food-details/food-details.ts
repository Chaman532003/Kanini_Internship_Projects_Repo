import { Component, computed, inject } from '@angular/core';
import { FoodService } from '../food.service';
import { FoodList } from '../food-list/food-list';

@Component({
  selector: 'app-food-details',
  imports: [],
  templateUrl: './food-details.html',
  styleUrl: './food-details.css'
})
export class FoodDetails {
foodservice = inject(FoodService)
// selectedFood = computed(()=>this.foodservice.selectedfood())
}
