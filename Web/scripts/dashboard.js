function dashboard() {
}

dashboard.prototype.draw = function(ctx,o) {
  var t = o || {
    "cap": {"x":200,"y":190,"radius":10},
    "needle": {"mark":60,"innerRadius":0,"outerRadius":160,"innerWidth":10,"outerWidth":5,"width":200,"height":190,"color":"black"},
    "radius":180,"x":200,"y":200,"background":"rgba(0,200,0,0.6)","hole":"white","holeRadius":120,'breakpoint':2,
    "sections":[
		{"portion":53, "color":"rgba(200,0,0,0.9)" },
		{"portion":75, "color":"rgba(150,150,0,0.9)" },
    ]};
  this.arc(ctx,t);
  this.needle(ctx,t.needle);
  this.cap(ctx,t.cap);
}

dashboard.prototype.cap = function (ctx,o) {
  var t = o;
  ctx.beginPath();
  ctx.arc(t.x,t.y,t.radius,0,-2*Math.PI,false);
  ctx.fill();
  ctx.closePath();
};

dashboard.prototype.needle = function (ctx, o) {
  var t = o;
  var v=t.mark*180/100.0-90;
  var vr=v/180*Math.PI;
  var r=t.outerRadius;
  var r2=t.innerRadius;
  var p1x=r*Math.sin(vr);
  var p1y=r*Math.cos(vr);
  var p2x=r2*Math.sin(vr);
  var p2y=r2*Math.cos(vr);
  var a=vr-Math.PI/2;
  var iw=t.innerWidth/2;
  var ow=t.outerWidth/2;
  var p3x=p2x+iw*Math.sin(a);
  var p3y=p2y+iw*Math.cos(a);
  var p4x=p1x+ow*Math.sin(a);
  var p4y=p1y+ow*Math.cos(a);
  a+=Math.PI;
  p1x+=ow*Math.sin(a);
  p1y+=ow*Math.cos(a);
  p2x+=iw*Math.sin(a);
  p2y+=iw*Math.cos(a);
  var w=t.width;
  var h=t.height;
  p1x=w+p1x;p1y=h-p1y;p2x=w+p2x;p2y=h-p2y;p3x=w+p3x;p3y=h-p3y;p4x=w+p4x;p4y=h-p4y;
  var pointsValue=p3x+","+p3y+" "+p4x+","+p4y+" "+p1x+","+p1y+" "+p2x+","+p2y+" "+p3x+","+p3y;
  ctx.fillStyle = t.color || "black";
  ctx.beginPath();
  ctx.moveTo(p3x,p3y);
  ctx.lineTo(p4x,p4y);
  ctx.lineTo(p1x,p1y);
  ctx.lineTo(p2x,p2y);
  ctx.lineTo(p3x,p3y);
  ctx.closePath();
  ctx.fill();
};

dashboard.prototype.arc = function(ctx,o) {
  var start = -Math.PI
  var t = o;

  for (var i =0; i < t.breakpoint; i++) {
   var p = t.sections[i];
   var s, e ;
   var radian = -Math.PI + p.portion * Math.PI/100.0;
   s = start;
   e = radian
   start = radian;
   ctx.fillStyle = p.color;
   ctx.beginPath();
   ctx.arc(t.x,t.y,t.radius, s, e, false);
   ctx.lineTo(t.x,t.y)
   ctx.fill();
  }
  ctx.fillStyle = t.background;
  ctx.beginPath();
  ctx.arc(t.x,t.y,t.radius, start, 0, false);
  ctx.lineTo(t.x,t.y)
  ctx.fill();
  ctx.closePath();
  ctx.beginPath();
  ctx.fillStyle = t.hole || "white";
  ctx.arc(t.x,t.y+1, t.holeRadius || t.radius/2, -Math.PI, 0, false);
  ctx.fill();
  ctx.closePath();
};

function loadcanvas(cid,mak,minv,lowr,midr,maxv,positive) {
	var canvas = null;
	canvas = document.getElementById(cid);
	if (canvas != null) {
		if (minv == maxv && minv == mak && mak == 0) { maxv = 0.0001; }
		lowr = ((lowr - minv) / (maxv - minv)) * 100;
		midr = ((midr - minv) / (maxv - minv)) * 100;
		if (mak < minv) { mak = minv; } else if (mak > maxv) { mak = maxv; }
		mak = ((mak - minv) / (maxv - minv)) * 100;
		var ctx = canvas.getContext('2d');
		if (positive == 'Y')
		{
		    var t = {'cap': {'x':200,'y':190,'radius':10},
		    'needle': {'mark':mak,'innerRadius':0,'outerRadius':160,'innerWidth':10,'outerWidth':5,'width':200,'height':190,'color':'black'},
		    'radius':180,'x':200,'y':200,'background':'rgba(0,200,0,0.6)','hole':'white','holeRadius':120,'breakpoint':2,
		    'sections':[{'portion':lowr, 'color':'rgba(200,0,0,0.9)' },{'portion':midr, 'color':'rgba(150,150,0,0.9)' },]};
		}
		else
		{
		    var t = {'cap': {'x':200,'y':190,'radius':10},
		    'needle': {'mark':mak,'innerRadius':0,'outerRadius':160,'innerWidth':10,'outerWidth':5,'width':200,'height':190,'color':'black'},
		    'radius':180,'x':200,'y':200,'background':'rgba(200,0,0,0.9)','hole':'white','holeRadius':120,'breakpoint':2,
		    'sections':[{'portion':lowr, 'color':'rgba(0,200,0,0.6)' },{'portion':midr, 'color':'rgba(150,150,0,0.9)' },]};
		}
		var d = new dashboard; d.draw(ctx,t);
	}
}
