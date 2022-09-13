from flask import Blueprint, render_template, request, flash, jsonify, redirect, url_for
from flask_login import login_required, current_user
from .models import User
from . import db
import json

views = Blueprint('views', __name__)

@views.route('/', methods=['GET', 'POST'])
@login_required
def home():
    if request.method == 'POST':
        flash(request.access_route, category='success')

    return render_template("home.html", user=current_user)

@views.route('/withdraw', methods=['GET', 'POST'])
@login_required
def withdraw():
    if request.method == 'POST':
        total_amount = current_user.account.funds
        withdrawn_amount = float(request.form.get('withdrawn_amount'))

        if withdrawn_amount <= 0:
            flash('Amount cannot be negative!', category='error')
        elif total_amount - withdrawn_amount < 0:
            flash('Withdrawl exceeds the current total funds!', category='error')
        else:
            current_user.account.funds = total_amount - withdrawn_amount
            db.session.commit()
            flash('Withdrawl successful!', category='success')
            return redirect(url_for("views.home"))
    
    return render_template("withdraw.html", user=current_user)

'''
@views.route('/delete-note', methods=['POST'])
def delete_item():
    item = json.loads(request.data)
    itemId = item['itemId']
    item = Item.query.get(itemId)
    if item:
        if item.user_id == current_user.id:
            db.session.delete(item)
            db.session.commit()
            flash(f'\'{item.name}\' successfully deleted!', category='success')

    return jsonify({})
'''