function colourItemFactory (colorItem, isSelected, onSelect) {	
	return (
		<Color item={colorItem} isSelected={isSelected} onSelectChanged={onSelect} /> 
	);
}

function colourItemMapper (colorItem) {
	return colourItemFactory(colorItem);
}

function isAccentColor (colorItem) {
	return colorItem.backgroundName.indexOf("Primary", 0) == -1;
}

var Color = React.createClass({
	_handleCheckChange: function(e) {
		if (!this.props.onSelectChanged)
			return;

		this.props.onSelectChanged(this.props.item, e.target.checked);			
	},
	getInitialState: function() {		
		return {isSelected: this.props.isSelected};
	},
	render: function() {
		var bgStyle = {
			background:this.props.item.backgroundColour			
		};
		var fgStyle = {
			color:this.props.item.foregroundColour,
			opacity:this.props.item.foregroundOpacity
		};

		var checkVis = (this.props.onSelectChanged)
			? "visible"
			: "hidden";
		var checkStyle = {
			visibility:checkVis
		};

		return (
			<div className="swatchColor" style={bgStyle}>
				<span style={fgStyle}>{this.props.item.backgroundName}</span>
				<input ref="checkbox" style={checkStyle} type="checkbox" onChange={this._handleCheckChange} checked={this.props.isSelected} />
			</div>
		);
	}
});

var SwatchSelector = React.createClass({
	handleUseClick: function(isAccent, e) {
		e.preventDefault();		
		this.props.onUseClick(isAccent, e);
	},
	render: function() {
		return (
			<div>Use <a href="#" onClick={this.handleUseClick.bind(this, false)}>Primary</a> <a href="#" onClick={this.handleUseClick.bind(this, true)}>Accent</a></div>
		);
	}
});

var Swatch = React.createClass({
	handleUseClick: function(index, isAccent, e) {		
		this.props.onUseClick(index, isAccent, e);		
	},
	render: function() {
		var colorNodes = this.props.colors.map(colourItemMapper);		
		return (
			<div className="swatch">
				{this.props.name}
				<SwatchSelector onUseClick={this.handleUseClick.bind(this, this.props.index)} />
				{colorNodes}
			</div>
		);
	}
});

var SwatchesBox = React.createClass({
	handleUseClick: function(index, isAccent, e) {
		this.props.onUseClick(index, isAccent);		
	},
	render: function() {
		var swatchNodes = this.props.swatches.map(function(swatch, index) {
			return (
				<Swatch name={swatch.name} colors={swatch.colors} index={index} onUseClick={this.handleUseClick} />
			);
		}, this);

		return (
			<div>
				{swatchNodes}
			</div>
		);
	}
});

function generateHuesState(primarySwatchIndex, accentSwatchIndex, primaryColorIndices, accentColorIndex) {
	var primaryColours = swatches[primarySwatchIndex].colors.filter(function(colorItem) {
		return colorItem.backgroundName.lastIndexOf("Primary") === 0;
	});
	var accentColours = swatches[accentSwatchIndex].colors.filter(function(colorItem) {
		return colorItem.backgroundName.lastIndexOf("Accent") === 0;
	});
	var primaryColorModels = primaryColours.map(function(colorItem, i) {
		return { 
			colorItem: colorItem, 
			isSelected: primaryColorIndices.indexOf(i) > -1
		}
	});
	var accentColorModels = accentColours.map(function(colorItem, i) {
		return { 
			colorItem: colorItem, 
			isSelected: accentColorIndex == i
		}
	});
	return {
		primaryColorModels: primaryColorModels,
		accentColorModels: accentColorModels,
		generatedFrom: {
			primarySwatchIndex: primarySwatchIndex, 
			accentSwatchIndex: accentSwatchIndex, 
			primaryColorIndices: primaryColorIndices, 
			accentColorIndex: accentColorIndex
		}
	};	
}

var HuesBox = React.createClass({
	getInitialState: function() {
		return generateHuesState(4, 10, [3,5,7], 2);
	},	
	handleColorSelect: function (colorIndex, color, isChecked) {		
		var newState;
		if (isAccentColor(color))			
			newState = generateHuesState(
				this.state.generatedFrom.primarySwatchIndex, 
				this.state.generatedFrom.accentSwatchIndex, 
				this.state.generatedFrom.primaryColorIndices, 
				colorIndex);
		else
		{
			if (this.state.generatedFrom.primaryColorIndices.indexOf(colorIndex) > -1 && isChecked)
				return;

			var newIndices = this.state.generatedFrom.primaryColorIndices.slice();
			newIndices.pop();
			newIndices.unshift(colorIndex);

			newState = generateHuesState(
				this.state.generatedFrom.primarySwatchIndex, 
				this.state.generatedFrom.accentSwatchIndex, 
				newIndices, 
				this.state.generatedFrom.accentColorIndex);
		}
		this.setState(newState);		
	},
	render: function() {
		var primaryColourNodes = this.state.primaryColorModels.map(function(model, i) {
			return colourItemFactory(model.colorItem, model.isSelected, this.handleColorSelect.bind(this, i));
		}, this);
		var accentColourNodes = this.state.accentColorModels.map(function(model, i) {
			return colourItemFactory(model.colorItem, model.isSelected, this.handleColorSelect.bind(this, i));
		}, this);
		return (
			<div>				
				{primaryColourNodes}
				{accentColourNodes}
			</div>
		);
	}
});

var huesBox = React.render(
	<HuesBox />,
	document.getElementById('hues')
	);

function mergeSwatch(index, isAccent) {	
	var state = huesBox.state;
	var newState = generateHuesState(
		isAccent ? huesBox.state.generatedFrom.primarySwatchIndex : index, 
		!isAccent ? huesBox.state.generatedFrom.accentSwatchIndex : index, 
		huesBox.state.generatedFrom.primaryColorIndices, 
		huesBox.state.generatedFrom.accentColorIndex);
	
	huesBox.setState(newState);
}

React.render(
	<SwatchesBox swatches={swatches} onUseClick={mergeSwatch} />,
	document.getElementById('swatches')
	);
