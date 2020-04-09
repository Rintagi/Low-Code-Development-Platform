/* manually created pages routing setup, you have to change as required */
import {CustomRoutePre as CustomPre, CustomRoutePost as CustomPost } from './Custom';

export const CustomRoutePre = [
  ...(CustomPre || []),
];

export const CustomRoutePost = [
  ...(CustomPost || []),
];

export const SuppressGenRoute = false;
