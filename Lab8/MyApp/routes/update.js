/**
 * Created by Owner on 9/12/2017.
 */
/**
 * Created by Owner on 9/12/2017.
 */
/**
 * Created by Owner on 9/12/2017.
 */
var express = require('express');
var router = express.Router();
var ObjectId = require('mongodb').ObjectID;

router.get('/', function(req, res, next){
    res.render('update', {da:{}});
})
router.post('/', function(req, res, next){
    console.log(req.body.name);
    db = req.db;
    var pointsDb = db.bind('point');
    var query = {$set:{name:req.body.name, category:req.body.category, location:{type:'point', coordinates:[req.body.longitude, req.body.latitude]}}};
    console.log(req.body.id);
    pointsDb.update({"_id":ObjectId(req.body.id)}, query, function(err, numdoc){
        //console.log(numdoc);
        pointsDb.close();
    });
    res.redirect('display');

});

module.exports = router;