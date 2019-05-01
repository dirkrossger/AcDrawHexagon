2019-04-24
Create points from calculated hexagon

2019-04-28
Look for duplicate coordinates

2019-04-29
https://stackoverflow.com/questions/18547354/c-sharp-linq-find-duplicates-in-list
- test queryX = points.GroupBy(x => x.X)
- test  queryY = points.GroupBy(x => x.Y)

2019-05-01
- Remove duplicate PointF(x, y) coordinates 
  -> remove coordinates exactly equal (all decimals)
  -> https://www.dotnetperls.com/duplicates

- Draw Circles
	-> http://csharphelper.com/blog/2016/11/circumscribe-three-tangent-circles-in-c/
