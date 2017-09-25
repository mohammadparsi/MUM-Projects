/**
 * Created by Owner on 9/11/2017.
 */
var express = require('express');
var router = express.Router();
const crypto = require('crypto');
const decipher = crypto.createDecipher('aes256', 'asaadsaad');
var ObjectId = require('mongodb').ObjectID;

router.get('/', function(req, res, next){
    var db = req.db;
    var collectionDb = db.bind('homework71');
    let decrypted = '';

    decipher.on('readable', () => {
        const data = decipher.read();
        if (data)
            decrypted += data.toString('utf8');
    });
    decipher.on('end', () => {
        res.send(decrypted);

    });
     var id ='59b6f132e6a50a6af25793c2';

        collectionDb.findOne({}, function(err, data){
             decipher.write(data.message, 'hex');
             decipher.end();
            return db.close();
        });

});

module.exports = router;