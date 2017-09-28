/**
 * Created by Owner on 9/25/2017.
 */

var express = require('express');
var router = express.Router();
var ObjectId = require('mongodb').ObjectId;

router.get('/getBooks', function(req, res, next) {
    console.log('from get Books')
    var documents=[];
    var dbs = req.db;
    dbs.collection('book').find({}).toArray(function(err, doc){
        if (err) throw err;
        doc.forEach(function(element){
            documents.push(element);
        });
        res.json(documents);
        dbs.close();
    })

});

router.get('/book/:id', function(req, res, next){

    var dbs = req.db;
    dbs.collection('book').findOne({_id:ObjectId(req.params.id)}, function(err, doc){
        if(err) throw err;

        res.json(doc);
        dbs.close();
    })
});

router.get('/ISBN', function(req, res, next){
    var dbs = req.db;
    dbs.collection('book').findOne({ISBN:req.params.isbn}, function(err, doc){
       if(err) throw err;
       res.json(doc);
       dbs.close();
    });

});

router.post('/postBook', function(req, res, next){

    var dbs = req.db;

    dbs.collection('book').insert( req.body, function(err, doc){
        if(err) throw err;
        console.log(doc)
        res.json(doc);
        dbs.close();
    });

});

router.post('/addReview', function(req, res, next){
    var dbs=req.db;
        console.log('from add review ')
        var email = req.body.email;
        var rev = req.body.reviewText;
        var counter = req.body.reviewId;
        //var revId = req.body.reviewId;
       var obj = {"reviewId": counter + "", "email": email, "review": rev}


            //console.log(req.body.bookid+" userId "+ usrId+" re "+rev+" obj "+obj.review);
            var operator = {'$push': {reviews: obj}};
            dbs.collection('book').update({_id: ObjectId(req.body.bookId)}, operator, function (err, doc) {
                if (err) throw err;
                dbs.collection('book').findOne({_id: ObjectId(req.body.bookId)}, function (err, dc) {
                    console.log("asdfasdf " + dc);
                    res.json(dc);
                    dbs.close();


                })


            });



    });

router.get('/getEditReview', function(req, res, next){
    var dbs = req.db;

    dbs.collection('book').findOne({_id:ObjectId(req.query.id)}, function(err, doc){
        if(err) throw err;
        //console.log(doc)
        var reviewsDoc = [];
        for(let i=0; i<doc.reviews.length; i++){
            if(doc.review[i].reviewId===req.query.reviewId){
                reviewsDoc = doc.review[i];
            }
        }


        var document = doc;
        var operator= {'$pull':{"reviews":reviewsDoc}};
        dbs.collection('book').update({_id:ObjectId(req.query.id)}, operator, function(err, doc){
            if(err) throw err;
            res.json(document);
            dbs.close();
        });
    });
})


router.post('/editReview', function(req, res, next) {
    console.log('from edit review method2')
    var dbs=req.db;
    console.log("Trial "+req.body.review);
    var email = req.body.email;
    var rev = req.body.review;
    var revId = req.body.reviewId;
    var obj = {"reviewId":revId, "email":email, "review" : rev};
//console.log(revId+" revId")
    var reviewId = 'reviews.'+revId;
    dbs.collection('book').findOne({_id:ObjectId(req.body.id)}, function(err, ele){


    var reviewDoc = {};
    for(let i=0; i<ele.reviews.length; i++){
        if(ele.reviews[i].reviewId===revId){
            reviewDoc = ele.reviews[i];
        }
    }
    //reviewDoc.review = req.body.review;
    console.log(reviewDoc);
    var operatorPull={'$pull':{"reviews":reviewDoc}};
        dbs.collection('book').update({_id:ObjectId(req.body.id)}, operatorPull, function(err, doc){
            if(err) throw err;
            console.log(reviewDoc);

            dbs.close();
        });
    var operatorPush= {'$push':{reviews:obj}};
    dbs.collection('book').update({_id:ObjectId(req.body.id)}, operatorPush, function(err, doc){
        if(err) throw err;
        console.log(obj.review);
        res.end();
        dbs.close();
    });///close
       dbs.close();
    });

})

module.exports = router;
