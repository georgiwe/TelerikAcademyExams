module.exports = {
	getErrorPage: function (req, res, next) {
		res.render('error');
	}
};