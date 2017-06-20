module.exports =  {
	itoa: function(variable){
		let temp = variable;
		return temp.toString();
	},
	atoi: function(variable){
		return variable.charCodeAt(0);
	},
	toInt: function(variable){
		return Math.floor(variable);
	}
}