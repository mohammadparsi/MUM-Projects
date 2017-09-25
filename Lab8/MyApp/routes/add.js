/**
 * Created by Owner on 9/12/2017.
 */
/**
 * Created by Owner on 9/12/2017.
 */
var express = require('express');
var router = express.Router();

router.get('/', function(req, res, next){
    res.render('add', {da:{}});
})
router.post('/', function(req, res, next){
    // console.log(req.body.name);
    db = req.db;
    var query = {name:req.body.name, category:req.body.category, location:{type:'Point', coordinates:[parseFloat(req.body.longitude), parseFloat(req.body.latitude)]}};
    // console.log(query);
    db.collection('point').insert(query, function(err, doc){
        if(err) console.log(err)
        console.log(doc);
    });
    res.redirect('display');

});

module.exports = router;