<%@ Control Language="c#" Inherits="RO.Web.ViewChartModule" CodeFile="ViewChartModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script type='text/javascript'>
    $(function () {
        var chartData = <%=hfChartData.Value%>
	$('#myflow')
		.myflow($.extend(true, {
		    basePath: '',
		    restore: chartData,
		    editable: false
		})
	);

	$('a[title!=\'\']').attr('id', 'CustomTooltip');

	$('a[title!=\'\']').each( function () {
		$(this).attr('tooltip', $(this).attr('title') );
		$(this).removeAttr('title');
	});
	
	$('a').each( function () {
	    var attr = $(this).attr('href');
	    var target = "_self";
	    if (typeof attr !== typeof undefined && attr !== "" && attr !== false) {
	        if (attr.toLowerCase().startsWith("https://") || attr.toLowerCase().startsWith("http://")) {
	            target = "_blank";
	            $(this).attr("target", "_blank");
	        }
	        $(this).next().next().next().attr('id', 'hyperLink').on("click", function () { window.open(attr, target); });
	        
	    } else {
	        $(this).removeAttr('href');
	    }
	});
	
	d3.selectAll('#hyperLink').each(function(d,i) {
	    d3.select(this).attr('text-decoration', 'underline');
	});

	d3.selectAll('#CustomTooltip').each(function(d,i) {
		d3.select(this).append('svg:circle')
		.attr('width', 8)
		.attr('height', 8)
		.attr('r', 8)
		.attr('cursor', 'pointer')
		.attr('fill', '#697C9E')
		.attr('cx', Math.round(d3.select(this).select('rect').attr('x')) + Math.round(d3.select(this).select('rect').attr('width')) - 12)
		.attr('cy', Math.round(d3.select(this).select('rect').attr('y')) + 11)

		d3.select(this).append('svg:text')
		.text('i')
		.attr('width', 8)
		.attr('height', 8)
		.attr('font-size', '11px')
		.attr('font-weight', 'bold')
		.attr('font-family', 'Helvetica')
		.attr('cursor', 'pointer')
		.attr('fill', '#ffffff')
		.attr('x', Math.round(d3.select(this).select('rect').attr('x')) + Math.round(d3.select(this).select('rect').attr('width')) - 14)
		.attr('y', Math.round(d3.select(this).select('rect').attr('y')) + 15)
	});

	var tooltip = d3.select('#myflow').append('div')
    .style('position', 'absolute')
    .style('visibility', 'hidden')
    .style('background-color', 'white')
    .style('border', 'solid')
    .style('border', '1px solid #cdcdcd')
    .style('border-radius', '5px')
    .style('padding', '10px')
	.style('color', '#666666')
	.style('min-width', '100px')
    .attr('class', "tooltipCnt");
	
	var cacheTitle = '';

	d3.selectAll('#CustomTooltip')
	.on('mouseover', function(){
		if(d3.select(this).attr('tooltip') != null){
			cacheTitle = d3.select(this).attr('tooltip').replace(/\n/g,'<br />');
			tooltip.html(cacheTitle);
			tooltip.style('visibility', 'visible');
		}
	})
	.on('mousemove', function(){
		if(d3.select(this).attr('tooltip') != null){
			tooltip.html(cacheTitle);
			tooltip.style('top', (event.pageY)+'px').style('left',(event.pageX)+ 10 +'px');
		}
	})
	.on('mouseout', function(){
		if(d3.select(this).attr('tooltip') != null){
			tooltip.style('visibility', 'hidden');
		}
	});
});
</script>

<style type='text/css'>
body {
  background-image: none;
}
    html, body, form, #myflow
    {
      height: 100%;
    }

    /*html
    {
        overflow:hidden;
    }*/

path, text{
    cursor: default !important;
}

    text#hyperLink
    {
        cursor: pointer !important;
    }
</style>
<asp:HiddenField ID="hfChartData" runat="server" />
<div id='myflow'></div>