import json
import boto3
import uuid

# DynamoDBクライアント
dynamodb = boto3.resource('dynamodb')
table = dynamodb.Table('ranking')  # DynamoDBテーブル名

def sort_score(target_score):

    # Scan操作で全アイテムを取得
    response = table.scan()
    
    # アイテムの取得
    items = response.get('Items', [])
    
    # スコアの降順でソート
    sorted_items = sorted(items, key=lambda x: x['score'], reverse=True)
    
    # 順位の付与
    for index, item in enumerate(sorted_items, start=1):
        item['rank'] = index
    
    # 結果の表示
    for item in sorted_items:
        print(f"ID: {item['id']}, Score: {item['score']}, Rank: {item['rank']}")
        if item['score'] == target_score:
            rank = item['rank']
            break
            
    return rank
    
def get_all_ranked_users():
    response = table.scan()
    items = response.get('Items', [])

    # `score`で降順にソート
    sorted_items = sorted(items, key=lambda x: x['score'], reverse=True)
    return sorted_items

def post_method(body):
    # メッセージとuserIdをログに出力
    print(f"Score: {body['score']}")
    print(f"UserName: {body['username']}")
    
    score = body['score']
    username = body['username']
    
    #DynamoDBに保存
    item = {
        'id': str(uuid.uuid4()),  # 一意のID
        'score': score,
        'username': username
    }
    table.put_item(Item=item)
    
    # レスポンスを返す
    return {
        'statusCode': 200,
        'headers': {
            'Content-Type': 'application/json'
        },
        'body': json.dumps({
            'method': "post"
        })
    }
    
def get_method():
    ranked_users = get_all_ranked_users()
    print('ranked_users:', ranked_users)
    
    # レスポンスを返す
    return {
        'statusCode': 200,
        'headers': {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',                 
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS',
            'Access-Control-Allow-Headers': 'Content-Type'
        },
        'body': json.dumps({
            'ranking': ranked_users
        })
    }

def lambda_handler(event, context):
    
    # HTTPメソッドを取得
    method = event.get("httpMethod", "")
    print(method)
    
    if method == "GET":
        # GETリクエストの処理
        response = get_method()
    elif method == "POST":
        # POSTリクエストの処理
        # event['body']からJSONメッセージをパース
        body = json.loads(event['body'])
        response = post_method(body)
    else:
        # その他のHTTPメソッドの場合
        response = {
            "error": f"Unsupported HTTP method: {method}"
        }
    return response
    
