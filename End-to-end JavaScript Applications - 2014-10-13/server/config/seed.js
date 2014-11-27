var data = require('../data');

var user = { 
	username: 'qweqwe',
	hashPass: 'qweqwe',
	firstname: 'Pesho',
	lastname: 'Peshov',
	phoneNumber: '958495',
	email: 'email@email.email',
	initiatives: [ 'Software Academy', 'Software Academy' ],
	seasons: [ 'Started 2010', 'Started 2011' ],
	profiles: { 
		fb: '', 
		google: '', 
		twitter: '', 
		linkedin: '' 
	} 
};

data.users.add(user);

// todo: add events