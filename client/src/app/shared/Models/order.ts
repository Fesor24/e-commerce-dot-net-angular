import { IAddress } from "./address";

    export interface IOrderCreate {
      cartId: string;
      deliveryMethodId: number;
      shipToAddress: IAddress;
  }


export interface IOrderItem {
    id: number;
    productName: string;
    pictureUrl: string;
    price: number;
    quantity: number;
}

export interface IOrder {
    id: number;
    buyerEmail: string;
    orderDate: string;
    shipToAddress: IAddress;
    deliveryMethods: string;
    orderItems: IOrderItem[];
    subTotal: number;
    status: number;
    paymentIntentId: string;
}
